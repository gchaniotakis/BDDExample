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

    [Trait("Authentication", "Email was not found")]
    public class EmailNotFound
    {
        AuthenticationResult _result;

        public EmailNotFound()
        {
            _result = new Authenticator().AuthenticateUser(new Credentials { Email = "g.chaniotakis@bewise.gr", Password = "password" });
        }

        [Fact(DisplayName = "Not Authenticated")]
        public void AuthDenied()
        {
            Assert.False(_result.Authenticated);
        }

        [Fact(DisplayName = "A message is returned explaining")]
        public void MessageReturned()
        {
            Assert.Contains("Invalid email", _result.Message);
        }
    }

    [Trait("Authentication", "Password doesn't match")]
    public class PasswordDoesntMatch : TestBase
    {
        AuthenticationResult _result;
        public PasswordDoesntMatch()
        {
            var app = new Application("g.chaniotakis@bewise.gr", "password", "password");
            new Registrator().ApplyForMembership(app);
            _result = new Authenticator().AuthenticateUser(new Credentials { Email = "g.chaniotakis@bewise.gr", Password = "321" });
        }

        [Fact(DisplayName = "Not authenticated")]
        public void NotAuthenticated()
        {
            Assert.True(_result.Authenticated);
        }

        [Fact(DisplayName = "MessageProvided")]
        public void MessageProvided()
        {
            Assert.Contains("Invalid", _result.Message);
        }
    }
}
