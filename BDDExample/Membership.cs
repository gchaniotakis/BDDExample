using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDDExample.Models;
using BDDExample.Services;

namespace BDDExample
{
    public class Membership : IMembership
    {
        private Authenticator _auth;
        private Registrator _reg;
        private Reminder _rem;
        public Membership(Authenticator auth, Registrator reg, Reminder rem)
        {
            auth = _auth;
            reg = _reg;
            rem = _rem;
        }
        public Membership()
        {
            _auth = new Authenticator();
            _reg = new Registrator();
            _rem = new Reminder();
        }

        public AuthenticationResult Authenticate(string email, string password, string ip = "192.168.64.1")
        {
            return _auth.AuthenticateUser(new Credentials(email, password, ip));
        }

        public User GetUser(string email)
        {
            return _auth.LocateUser(email);
        }

        public AuthenticationResult AuthenticateByOpenAuth(string providerid, string ip = "192.168.64.1")
        {
            return _auth.AuthenticateUserByOpenAuth(providerid, ip);
        }

        public AuthenticationResult AuthenticateByToken(string authenticationtoken, string ip = "192.168.64.1")
        {
            return _auth.AuthenticateUserByToken(authenticationtoken, ip);
        }

        public RegistrationResult Register(string email, string password, string confirm, string publicname = null, string ip = "192.168.64.1")
        {
            var app = new Application(email, password, confirm);
            app.PublicName = publicname;
            return _reg.ApplyForMembership(app);
        }

        public ReminderResult SendResetTokenToUser(string email, string reseturl = "http://localhost/reminder/")
        {
            return _rem.SendReminderTokenToUser(email, reseturl);
        }

        public ResetResult ResetUsersPassword(string resettoken, string newpassword)
        {
            return _rem.ResetUserPassword(Guid.Parse(resettoken), newpassword);
        }

        public bool AssociateOpenAuth(string email, string provider, string providerid)
        {
            return _auth.AssociateOpenAuth(email, provider, providerid);
        }

        public bool DisassociateOpenAuth(string email, string providerid)
        {
            return _auth.DisassociateOpenAuth(email, providerid);
        }

    }
}
