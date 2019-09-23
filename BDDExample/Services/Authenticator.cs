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
            var user = new User();
            user.AddLogEntry("Login", "User loggeed in");
            result.Authenticated = true;
            result.User = user;
            return result;
        }
    }
}
