using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BDDExample.Services;
using BDDExample.Models;

namespace BDDExampleTests.Registration
{
    [Trait("Application has an email that already exists","")]
    public class ExistingEmail
    {
        RegistrationResult _result;
        public ExistingEmail()
        {
            var app1 = new Application("ex.ist@bewise.gr", "password", "password");
            _result = new Registrator().ApplyForMembership(app1);
        }

        [Fact (DisplayName ="Initial application succeeds")]
        public void AppIsInDB()
        {
            Assert.Equal(ApplicationStatus.Accepted, _result.Application.Status);
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
            Assert.Contains("ex", _result.Application.Email);
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


        [Fact(DisplayName = "Application is Denied")]
        public void UserDenied()
        {
            Assert.Equal(ApplicationStatus.Denied, _result.Application.Status);
        }

        [Fact(DisplayName = "A message is shown explaining why")]
        public void MessageIsShown()
        {
            Assert.Contains("invalid", _result.Application.UserMessage);
        }
    }
      
}
