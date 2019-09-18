using System;
using System.Collections.Generic;
using System.Text;
using BDDExample.Models;

namespace BDDExample.Services
{
    public class RegistrationResult
    {
        public User NewUser { get; set; }
        public Application Application { get; set; }



    }

    public class Registrator
    {
        public virtual Application ValidateApplication(Application app)
        {
            if (app.Email.Length < 6)
            {
                app.IsValid = false;
                app.UserMessage = Properties.Resources.InvalidEmailMessage;
                app.Status = ApplicationStatus.Denied;
            }
            else
            {
                app.IsValid = true;
            }   
            return app;
        }
        public RegistrationResult ApplyForMembership(Application app)
        {
            var result = new RegistrationResult();
            result.Application =ValidateApplication(app);
            if(result.Application.IsValid)
            {
                result.Application.UserMessage = "Welcome";
                result.NewUser = new User();
                result.NewUser.Logs.Add(new UserActivityLog { Subject = "Registration", Entry = "User" + result.NewUser.Email + "successfully registered!", UserId = result.NewUser.Id });
                result.NewUser.MailerLogs.Add(new UserMailerLog { Subject = "Please confirm your E-mail", Body = "Lorem Ipsum", UserId = result.NewUser.Id });
                result.Application.IsValid = true;
                result.Application.Status = ApplicationStatus.Accepted;
            }
            else
            {
                result.Application.IsValid = false;
                result.Application.UserMessage = "Your application is invalid.";
            }

            return result;

        }
    }

}
