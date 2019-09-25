using BDDExample.DB;
using BDDExample.Models;
using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDDExample.Services
{
    public class AuthenticationResult
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Authenticator
    {
        private Credentials CurrentCredentials;
        private ApplicationDbContext _db;

        public AuthenticationResult AuthenticateUser(Credentials creds)
        {
            var result = new AuthenticationResult();
            CurrentCredentials = creds;
            if (EmailOrPasswordNotPresent())
                return InvalidLogin("Email and Password are required to log in");
            var user = LocateUser();
            if (user == null)
                return InvalidLogin("Invalid email or password");
            if (HashedPasswordDoesNotMatch(user))
                return InvalidLogin("Invalid password");
            user.AddLogEntry("Login", "User loggeed in");
            CreateSession(user);
            UserAuthenticated(user);
            result.Authenticated = true;
            result.User = user;
            result.Message = "Welcome back!";
            return result;
        }

        public virtual bool HashedPasswordDoesNotMatch(User user)
        {
            return !BCryptHelper.CheckPassword(CurrentCredentials.Password, user.HashedPassword);
        }

        private AuthenticationResult InvalidLogin(string message)
        {
            if (_db != null)
                _db.Dispose();
            return new AuthenticationResult { Message = message, Authenticated = false };
        }

        public virtual void CreateSession(User user)
        {
            user.Sessions.Add(new UserSession(user.Id));
        }

        private bool EmailOrPasswordNotPresent()
        {
            return string.IsNullOrWhiteSpace(CurrentCredentials.Email) || string.IsNullOrWhiteSpace(CurrentCredentials.Password);
        }

        public virtual User LocateUser()
        {
            return _db.Users.FirstOrDefault(x => x.Email == CurrentCredentials.Email);
        }

        public virtual void UserAuthenticated(User user)
        {
            _db.SaveChanges();
            _db.Dispose();
        }

        
    }
}
