using BDDExample.Models;
using BDDExample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace BDDExampleTests.Registration
{
    
    [Trait("A Valid Application is Submitted", "")]
    public class ValidApplicationReceived
    {
        Registrator _reg;
        RegistrationResult _result;
        User _user;

        public ValidApplicationReceived()
        {
            _reg = new Registrator();
            var app = new Application(email: "g.chaniotakis@bewise.gr", password:"password",confirm:"password");
            _result = _reg.ApplyForMembership(app);
            _user = _result.NewUser;
        }

        [Fact(DisplayName ="A user is added to the system")]
        public void UserIsAddedToSystem()
        {
            Assert.NotNull(_user);
        }

        [Fact(DisplayName ="User status set to pending")]
        public void UserStatusSetToPending()
        {
            Assert.Equal(UserStatus.Pending, _user.Status);
        }

        [Fact(DisplayName ="Log entry created for event")]
        public void LogEntryIsCreatedForEvent()
        {
            Assert.Equal(1,_user.Logs.Count);
        }

        [Fact(DisplayName ="Email is sent to confirm address")]

        public void EmailSentToConfirmAddress()
        {
            Assert.Equal(1, _user.MailerLogs.Count);
        }

        [Fact(DisplayName ="A confirmation message is provided to show to the user")]

        public void AMessageIsProvidedForUser()
        {
            Assert.Equal("Welcome", _result.Application.UserMessage);
        }

        [Fact(DisplayName ="Application is Validated")]
        public void ApplicationValidated()
        {
            Assert.True(_result.Application.IsValid);
        }
       
    }
}
