using System;
using System.Collections.Generic;
using System.Text;
using BDDExample.Models;

namespace BDDExample.Services
{
    public class RegistrationResult
    {
        public User NewUser { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public Application Application { get; set; }

        public RegistrationResult()
        {
            Success = false;
        }

    }

    public class Registrator
    {
        public virtual Application ValidateApplication(Application app)
        {
            app.IsValid = true;
            return app;
        }
        public RegistrationResult ApplyForMembership(Application app)
        {
            var result = new RegistrationResult();
            result.Application = app;
            result.Message = "Welcome";
            result.NewUser = new User();
            result.NewUser.Logs.Add(new UserActivityLog { Subject = "Registration", Entry = "User" + result.NewUser.Email + "successfully registered!", UserId = result.NewUser.Id });
            result.NewUser.MailerLogs.Add(new UserMailerLog { Subject = "Please confirm your E-mail", Body = "Lorem Ipsum", UserId = result.NewUser.Id });
            return result;
        }
    }

}
