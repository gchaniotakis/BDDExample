using BDDExample.Models;
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
            var user = new User();
            user.AddLogEntry("Login", "User loggeed in");
            CreateSession(user);
            result.Authenticated = true;
            result.User = user;
            result.Message = "Welcome back!";
            return result;
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
    }
}
