using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BDDExample.Services;
using BDDExample.Models;

namespace BDDExampleTests.Registration
{
    [Trait("An application is received with empty email or password", "")]
    public class EmptyEmailOrPassword
    {
        RegistrationResult _result;
        public EmptyEmailOrPassword()
        {
            var app = new Application("","password","password");
        }

        [Fact(DisplayName = "An exception is thrown with empty email")]
        public void ApplicationInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => new Application("", "password", "password"));
        }

        [Fact(DisplayName = "An exception is thrown with empty password")]
        public void MessageReturned()
        {
            Assert.Throws<InvalidOperationException>(() => new Application("g.chaniotakis@bewise.gr", "", "password"));
        }
    }
}
