using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BDDExample.Services;
using BDDExample.Models;

namespace BDDExampleTests.Registration
{
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
