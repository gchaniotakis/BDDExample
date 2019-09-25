using BDDExample.Models;
using BDDExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BDDExampleTests.Authentication
{
    [Trait("Authentication", "Valid Login")]
    public class ValidLogin : TestBase
    {
        AuthenticationResult _result;

        public ValidLogin()
        {
            var app = new Application("g.chaniotakis@bewise.gr", "password", "password");
            new Registrator().ApplyForMembership(app);
            var auth = new Authenticator();
            _result = auth.AuthenticateUser(new Credentials{Email = "g.chaniotakis@bewise.gr", Password = "password"});
        }

        [Fact(DisplayName ="User authenticated")]
        public void AuthenticateUser()
        {
            Assert.True(_result.Authenticated);
        }

        [Fact(DisplayName ="User is returned")]
        public void UserReturned()
        {
            Assert.NotNull(_result.User);
        }

        [Fact(DisplayName ="Log entry created")]
        public void CreateLogEntry()
        {
            Assert.True(_result.User.Logs.Count > 0);
        }

        [Fact(DisplayName ="A session is created")]
        public void SessionCreated()
        {
            Assert.True(_result.User.Sessions.Count > 0);
        }

        [Fact(DisplayName ="User has current session")]
        public void RememberMeTokenCreated()
        {
            Assert.NotNull(_result.User.CurrentSession);
        }

        [Fact(DisplayName ="Session expires in 30 days")]
        public void RememberMeExpiresIn30Days()
        {
            Assert.True(_result.User.CurrentSession.FinishedAt == DateTime.Today.AddDays(30));
        }



        [Fact(DisplayName ="A welcome message is provided")]
        public void WelcomeMessageProvided()
        {
            Assert.Contains("Welcome", _result.Message);
        }
        
    }
}
