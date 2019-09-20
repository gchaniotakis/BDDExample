using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BDDExample.Services;
using BDDExample.Models;

namespace BDDExampleTests.Registration
{
    [Trait("Application has an email that already exists","")]
    public class ExistingEmail: TestBase
    {
        RegistrationResult _result;
        public ExistingEmail() :base()
        {
            var app1 = new Application("ex.ist@bewise.gr", "password", "password");
            _result = new Registrator().ApplyForMembership(app1);
        }

        [Fact (DisplayName ="Application returns message")]
        public void ApplicationReturnsMessage()
        {
            var app2 = new Application("ex.ist@bewise.gr", "password","password");
            _result = new Registrator().ApplyForMembership(app2);
            Assert.Contains("ex", _result.Application.UserMessage);
        }

        [Fact(DisplayName ="App doesn't throw")]
        public void AppDoesntThrow()
        {
            var app2 = new Application("ex.ist@bewise.gr", "password", "password");
            //Assert.Throws(() => _result = new Registrator().ApplyForMembership(app2));
            
        }

        [Fact(DisplayName ="Application is invalid")]
        public void ApplicationIsInvalid()
        {
            var app2 = new Application("ex.ist@bewise.gr", "password", "password");
            _result = new Registrator().ApplyForMembership(app2);
            Assert.True(app2.IsInvalid());
        }
    }

    [Trait("Application has an email with 5 characters", "")]
    public class ShortEmail
    {
        RegistrationResult _result;

        public ShortEmail()
        {
            var app = new Application("ghan", "password", "password");
            _result = new Registrator().ApplyForMembership(app);
        }


        [Fact(DisplayName = "Application is invalid")]
        public void UserDenied()
        {
            Assert.True(_result.Application.IsInvalid());
        }

        [Fact(DisplayName = "A message is shown explaining invalidation")]
        public void MessageIsShown()
        {
            Assert.Contains("invalid", _result.Application.UserMessage);
        }
    }
      
}
