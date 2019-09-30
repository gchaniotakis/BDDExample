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
        public UserSession Session {get; set;}
    }

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public bool RememberMe { get; set; }
        public Guid Token { get; set; }

        public Credentials(string email, string password,string IP = "192.168.64.1")
        {
            Email = email;
            Password = password;
            this.IP = IP;
        }

        public Credentials()
        {
            IP = "192.168.64.1";
            RememberMe = true;
        }
    }

    public class Authenticator
    {
        private Credentials CurrentCredentials;
        private ApplicationDbContext _db;

        public AuthenticationResult AuthenticateUser(Credentials creds)
        {
            var result = new AuthenticationResult();
            User user = null;
            CurrentCredentials = creds;
            if(EmailOrPasswordNotPresent())
            {
                result = InvalidLogin(Properties.Resources.EmailOrPasswordMissing);
            }

            else
            {
                user = LocateUser(creds.Email);
                
                if (user == null)
                {
                    result = InvalidLogin(Properties.Resources.InvalidLogin);
                }

                else if (HashedPasswordDoesNotMatch(user))
                {
                    result = InvalidLogin(Properties.Resources.InvalidLogin);
                }

                else
                {
                    user.AddLogEntry("Login", "User logged in");
                    result.Session = CreateSession(user);
                    SetUserLoginStats(user);
                    UserAuthenticated(user);
                    result.Authenticated = true;
                    result.User = user;
                    result.Message = Properties.Resources.UserAuthenticated;
                    _db.SaveChanges();
                }
            }

            _db.Dispose();
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

        public virtual UserSession CreateSession(User user)
        {
            var session = new UserSession { IP = CurrentCredentials.IP };
            session.FinishedAt = CurrentCredentials.RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(1);
            user.Sessions.Add(session);
            return session;
        }

        private bool EmailOrPasswordNotPresent()
        {
            return string.IsNullOrWhiteSpace(CurrentCredentials.Email) || string.IsNullOrWhiteSpace(CurrentCredentials.Password);
        }

        public virtual User LocateUser(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == CurrentCredentials.Email);
        }

        public virtual void UserAuthenticated(User user)
        {

        }

        public virtual void SetUserLoginStats(User user)
        {
            user.SignInCount += 1;
            user.CurrentSignInAt = user.LastSignInAt;
            user.LastSignInAt = DateTime.Now;
            user.IP = CurrentCredentials.IP;
        }

        public virtual User FindUserByAuthenticationToken()
        {
            return _db.Users.FirstOrDefault(t => t.AuthenticationToken == CurrentCredentials.Token);
        }

        public virtual User GetCurrentUser(Guid token)
        {
            User user = null;
            var validsession = _db.Sessions.FirstOrDefault(s => s.Id == token && s.FinishedAt > DateTime.Now);
            if (validsession != null)
            {
                user = validsession.User;
            }
            return user;
        }

        public virtual bool EndUserSession(Guid sessiontoken)
        {
            var result = false;
            var usersession = _db.Sessions.FirstOrDefault(s => s.Id == sessiontoken);
            if(usersession != null)
            {
                usersession.FinishedAt = DateTime.Now;
                _db.SaveChanges();
                result = true;
            }

            return result;
        }

        public AuthenticationResult AuthenticateUserByToken(string token, string ip = "192.168.64.1")
        {
            var result = new AuthenticationResult();
            
            if (string.IsNullOrWhiteSpace(token))
            {
                result = InvalidLogin("No token provided");
            }
            
            else
            {
                CurrentCredentials = new Credentials { Token = Guid.Parse(token), IP = ip };

                var user = FindUserByAuthenticationToken();
                if(user == null)
                {
                    result = InvalidLogin("Invalid token");
                }

                else
                {
                    user.AddLogEntry("Login", "User logged in by token");
                    result.Session = CreateSession(user);
                    SetUserLoginStats(user);
                    UserAuthenticated(user);

                    result.Authenticated = true;
                    result.User = user;
                    result.Message = Properties.Resources.UserAuthenticated;
                    _db.SaveChanges();
                }
            }

            _db.Dispose();
            return result;
        }



        
    }
}
