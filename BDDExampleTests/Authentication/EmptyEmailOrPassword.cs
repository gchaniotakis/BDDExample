using BDDExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BDDExampleTests.Authentication
{

    [Trait("Authentication", "Empty email or password")]
    public class EmptyEmail :TestBase
    {
        AuthenticationResult _result;
        public EmptyEmail()
        {
            _result = new Authenticator().AuthenticateUser(new Credentials { Email = "" });
        }

        [Fact(DisplayName ="Not Authenticated")]
        public void AuthDenied()
        {
            Assert.False(_result.Authenticated);
        }

        [Fact(DisplayName ="A message is returned explaining")]
        public void MessageReturned()
        {
            Assert.Contains("required", _result.Message);
        }
    }    public class EmptyPassword :TestBase
    {
        AuthenticationResult _result;
        public EmptyPassword()
        {
            _result = new Authenticator().AuthenticateUser(new Credentials { Email = "g.chaniotakis@bewise.gr",Password = "" });
        }

        [Fact(DisplayName ="Not Authenticated")]
        public void AuthDenied()
        {
            Assert.False(_result.Authenticated);
        }

        [Fact(DisplayName ="A message is returned explaining")]
        public void MessageReturned()
        {
            Assert.Contains("required", _result.Message);
        }
    }
}
