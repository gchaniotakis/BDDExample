using System;
using System.Collections.Generic;
using System.Data.Entity;
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



    }

    public class Registrator
    {
        Application CurrentApplication;

        bool EmailOrPasswordNotPresent()
        {
            return string.IsNullOrWhiteSpace(CurrentApplication.Email) || string.IsNullOrWhiteSpace(CurrentApplication.Password);
        }

        public virtual bool EmailAlreadyRegistered()
        {
            var exists = false;
            using (var db = new ApplicationDbContext())
            {
                exists = db.Users.FirstOrDefaultAsync(x => x.Email == CurrentApplication.Email) != null;
            }

            return exists;
        }

        public virtual bool EmailIsInvalid()
        {
            return CurrentApplication.Email.Length <= 5;
        }

        public virtual bool PasswordIsInvalid()
        {
            return CurrentApplication.Password.Length <= 4;
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
            var user = new User { Email = CurrentApplication.Email, HashedPassword = HashPassword() };
            using(var db = new ApplicationDbContext())
            {
                
                user.Status = UserStatus.Pending;
                


            }
            return user;
        }

        public virtual void SaveNewUser(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
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
            CurrentApplication = app;
            var result = new RegistrationResult();
            result.Application = app;
            result.Application.UserMessage = "Welcome!";

            if (EmailOrPasswordNotPresent())
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

            result.NewUser = AcceptApplication();             
            return result;
        }
    }

}
