using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using BDDExample.DB;
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
        Application _app;

        bool EmailOrPasswordNotPresent()
        {
            return string.IsNullOrWhiteSpace(_app.Email) || string.IsNullOrWhiteSpace(_app.Password);
        }

        public virtual bool EmailAlreadyRegistered()
        {
            var exists = false;
            using (var db = new ApplicationDbContext())
            {
                exists = db.Users.FirstOrDefaultAsync(x => x.Email == _app.Email) != null;
            }

            return exists;
        }

        public virtual bool EmailIsInvalid()
        {
            return _app.Email.Length <= 5;
        }

        public virtual bool PasswordIsInvalid()
        {
            return _app.Password.Length <= 4;
        }

        public virtual bool PasswordMatchesConfirmation()
        {
            return _app.Password.Equals(_app.Confirmation);
        }

        public RegistrationResult InvalidApplication(string reason)
        {
            var result = new RegistrationResult();
            _app.Status = ApplicationStatus.Invalid;
            result.Application = _app;
            result.Application.UserMessage = reason;
            return result;
            
        }

        public virtual RegistrationResult ApplicationAccepted()
        {
            var result = new RegistrationResult();
            using (var db = new ApplicationDbContext())
            {
                _app.Status = ApplicationStatus.Invalid;
                result.Application = _app;
                result.Application.UserMessage = "Welcome!";
                var user = new User { Email = _app.Email };
                user.Logs.Add(new UserActivityLog { Subject = "Registration", Entry = "User " + user.Email + " was successfully created" });
                user.Status = UserStatus.Pending;
                user.MailerLogs.Add(new UserMailerLog { Subject = "Email confirmation", Body = "Dear user " + user.Email + " follow this link to confirm your email" });
                db.Users.Add(user);
                db.SaveChanges();
                result.NewUser = user;
            }

            return result;        
            


        }

        public RegistrationResult ApplyForMembership(Application app)
        {
            _app = app;

            var resutlt = new RegistrationResult();

            if(EmailOrPasswordNotPresent())
            {
                return InvalidApplication("Email and password are required");
            }

            if(EmailIsInvalid())
            {
                return InvalidApplication("Email is not valid");
            }

            if(PasswordIsInvalid())
            {
                return InvalidApplication("Password is invalid");
            }

            if(!PasswordMatchesConfirmation())
            {
                return InvalidApplication("The two passwords don't match");
            }

            if(EmailAlreadyRegistered())
            {
                return InvalidApplication("This email already exists");
            }

            return ApplicationAccepted();
        }
    }

}
