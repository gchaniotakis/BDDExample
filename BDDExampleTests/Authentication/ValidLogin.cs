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
    public class ValidLogin
    {
        AuthenticationResult _result;

        public ValidLogin()
        {
            var auth = new Authenticator();
            _result = auth.AuthenticateUser(new Credentials{Email = "g.chaniotakis@bewise.gr", Password = "password"});
        }

        [Fact(DisplayName ="User authenticated")]
        public void AuthenticateUser()
        {
            Assert.True(_result.Authenticated);
        }

        [Fact(DisplayName ="Log entry created")]
        public void CreateLogEntry()
        {
            Assert.True(_result.User.Logs.Count > 0);
        }

        [Fact(DisplayName ="Remember me token is created")]
        public void RememberMeTokenCreated()
        {

        }

        [Fact(DisplayName ="Remember me expires in 30 days")]
        public void RememberMeExpiresIn30Days()
        {

        }

        [Fact(DisplayName ="User is returned")]
        public void UserReturned()
        {

        }

        [Fact(DisplayName ="A welcome message is provided")]
        public void WelcomeMessageProvided()
        {

        }
        
    }
}
