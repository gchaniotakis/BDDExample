using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BDDExampleTests.Registration
{
    public enum UserStatus
    {
        Pending
    }

    public class UserActivityLog
    {
        public string Subject { get; set; }
        public string Entry { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserActivityLog()
        {
            CreatedAt = DateTime.Now;
        }
    }

    public class UserMailerLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserMailerLog()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }

    public class User
    {
       public User()
        {
            Status = UserStatus.Pending;
            Id = Guid.NewGuid();
            Logs = new List<UserActivityLog>();
            MailerLogs = new List<UserMailerLog>();
            CreatedAt = DateTime.Now;
        }

        public UserStatus Status;
        public Guid Id { get; set; }
        public ICollection<UserActivityLog> Logs { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserMailerLog> MailerLogs { get; set; }
    }

    public class RegistrationResult
    {
        public User NewUser { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public RegistrationResult()
        {
            Success = false; 
        }

    }

    public class Registrator
    {
        public RegistrationResult ApplyForMembership()
        {
            var result = new RegistrationResult();
            result.Message = "Welcome";
            result.NewUser = new User();
            result.NewUser.Logs.Add(new UserActivityLog { Subject = "Registration", Entry = "User" + result.NewUser.Email + "successfully registered!", UserId = result.NewUser.Id });
            result.NewUser.MailerLogs.Add(new UserMailerLog { Subject = "Please confirm your E-mail", Body = "Lorem Ipsum", UserId = result.NewUser.Id } );
            return result;
        }
    }



    [Trait("A Valid Application is Submitted", "")]
    public class ValidApplicationReceived
    {
        Registrator _reg;
        RegistrationResult _result;
        User _user;

        public ValidApplicationReceived()
        {
            _reg = new Registrator();
            _result = _reg.ApplyForMembership();
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
            Assert.Equal("Welcome", _result.Message);
        }

       
    }
}
