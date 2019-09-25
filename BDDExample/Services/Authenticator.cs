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
        Credentials CurrentCredentials;


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
            SaveUserLogin(user);
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
            User user = null;
            using(var db = new ApplicationDbContext() )
            {
                user = db.Users.FirstOrDefault(x => x.Email == CurrentCredentials.Email);                
            }

            return user;
        }

        public virtual void SaveUserLogin(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.SaveChanges();
            }
        }
    }
}
