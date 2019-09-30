using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BDDExample.DB;
using BDDExample.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace BDDExample.Services
{
    public class RegistrationResult
    {
        public User NewUser { get; set; }
        public Application Application { get; set; }
        public Guid SessionToken { get; set; }
        public bool Successful
        {
            get
            {
                return Application == null ? false : Application.IsAccpeted(); 
            }
        }
    }

    public class Registrator
    {
        private Application CurrentApplication;
        private ApplicationDbContext _db;
        private Configuration _config;
        public Registrator(Configuration config = null)
        {
            _config = config ?? new Configuration();
        }

        bool EmailOrPasswordNotPresent()
        {
            return string.IsNullOrWhiteSpace(CurrentApplication.Email) || string.IsNullOrWhiteSpace(CurrentApplication.Password);
        }

        public virtual bool EmailAlreadyRegistered()
        {
            var exists = false;            
            exists = _db.Users.FirstOrDefault(x => x.Email == CurrentApplication.Email) != null;
            return exists;
        }

        public virtual bool EmailIsInvalid()
        {
            return CurrentApplication.Email.Length <= 5;
        }

        public virtual bool PasswordIsInvalid()
        {
            return CurrentApplication.Password.Length <= _config.MinPasswordLength;
        }

        public virtual bool PasswordMatchesConfirmation()
        {
            return CurrentApplication.Password.Equals(CurrentApplication.Confirmation);
        }

        public RegistrationResult InvalidApplication(string reason)
        {
            var result = new RegistrationResult();
            CurrentApplication.Status = ApplicationStatus.Invalid;
            result.Application = CurrentApplication;
            result.Application.UserMessage = reason;
            return result;
            
        }

        public virtual string HashPassword()
        {
           return BCryptHelper.HashPassword(CurrentApplication.Password, BCryptHelper.GenerateSalt(10));
        }

        public virtual void SendConfirmationRequest(User user)
        {
            user.MailerLogs.Add(new UserMailerMessage { Subject = "Email confirmation", Body = "Dear user " + user.Email + " follow this link to confirm your email" });
        }

        public virtual User CreateUserFromApplication()
        {
            return new User
            {
                Email = CurrentApplication.Email,
                HashedPassword = HashPassword(),
                Status = UserStatus.Pending
            };
        }

        public virtual void SaveNewUser(User user)
        {     
            _db.Users.Add(user);
            _db.SaveChanges();            
        }

        public virtual User AcceptApplication()
        {
           

            //set the status
            CurrentApplication.Status = ApplicationStatus.Accepted;



            //create the new user
            var user = CreateUserFromApplication();

            //log the registration            
            user.AddLogEntry("Registration", "User " + user.Email +" was succesfully created");

            //send email
            SendConfirmationRequest(user);            
            user.AddLogEntry("Registration",  "Email confirmation request sent");

            //save user
            SaveNewUser(user);
            return user;

        }

        public RegistrationResult ApplyForMembership(Application app)
        {
            var result = new RegistrationResult();
            CurrentApplication = app;            
            result.Application = app;
            result.Application.UserMessage = "Welcome!";

            if (EmailOrPasswordNotPresent())
            {
                return InvalidApplication(Properties.Resources.EmailOrPasswordMissing);
            }

            if(EmailIsInvalid())
            {
                return InvalidApplication(Properties.Resources.InvalidEmailMessage);
            }

            if(PasswordIsInvalid())
            {
                return InvalidApplication(Properties.Resources.InvalidPassword);
            }

            if(!PasswordMatchesConfirmation())
            {
                return InvalidApplication(Properties.Resources.PasswordConfirmationMissmatch);
            }

            if(EmailAlreadyRegistered())
            {
                return InvalidApplication(Properties.Resources.EmailExists);
            }

            result.NewUser = AcceptApplication();
            var auth = new Authenticator().AuthenticateUser(new Credentials { Email = result.NewUser.Email, Password = CurrentApplication.Password });
            result.SessionToken = auth.Session.Id;
            return result;
        }
    }

}
