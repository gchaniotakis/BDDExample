using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDDExample.Models;
using BDDExample.DB;
using DevOne.Security.Cryptography.BCrypt;

namespace BDDExample.Services
{
    public class ResetResult
    {
        public bool Succesful { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }

    public class ReminderResult
    {
        public string Message { get; set; }
        public Guid Token { get; set; }
        public DateTime SentAt { get; set; }
        public UserMailerMessage MailMessage { get; set; }
        public User User { get; set; }
        public bool Successful
        {
            get
            {
                return MailMessage == null ? false : MailMessage.Succesful;
            }
        }
    }

    public class Reminder
    {
        private ApplicationDbContext _db;
        private Configuration _config;
        public Authenticator(Configuration config = null)
        {
            _config = config ?? new Configuration();
        }

        public virtual UserMailerTemplate GetReminderMailer()
        {
            return _db.Mailers.FirstOrDefault(m => m.MailerType == MailerType.ReminderToken);
        }

        public virtual string CreateReminderLink(User user , string rootUrl = "http://localhost/reminder/")
        {
            return rootUrl + "?t=" + user.ReminderToken;
        }

        public virtual User GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }

        public ReminderResult SendReminderTokenToUser(string  email, string rootUrl)
        {            
            var result = new ReminderResult();
            result.User = GetUserByEmail(email);

            if(result.User != null)
            {
                result.User.ReminderToken = Guid.NewGuid();
                result.User.ReminderSentAt = DateTime.Now;

                var mailer = GetReminderMailer();
                var message = new UserMailerMessage();
                var link = CreateReminderLink(result.User,rootUrl);

                message.Subject = mailer.Subject;
                message.Body = mailer.FormatBody(link);
                message.Mailer = mailer;

                if(message.Succesful)
                {
                    result.User.AddLogEntry("Login", "Reminder email sent at" + DateTime.Now.ToShortDateString());
                }

                else
                {
                    result.User.AddLogEntry("Login", "Reminder email failed to send at" + DateTime.Now.ToShortDateString());
                }

                result.MailMessage = message.SendTo(result.User);
                _db.SaveChanges();
            }

            else
            {
                result.Message = Properties.Resources.EmailNotFound;
            }

            _db.Dispose();
            return result;
        }

        public virtual bool ResetWindowIsOpen(User user)
        {
            return user.ReminderSentAt > DateTime.Now.AddHours(-12);
        }

        public virtual bool PasswordResetIsValid(string newpassword)
        {
            return !string.IsNullOrWhiteSpace(newpassword) && newpassword.Length > 4;
        }

        public ResetResult ResetUserPassword(Guid token, string newpassword)
        {
            var result = new ResetResult();            
            var user = GetUserByToken(token);
            if(user !=null)
            {
                if(PasswordResetIsValid(newpassword))
                {
                    if(ResetWindowIsOpen(user))
                    {
                        var hashed = BCryptHelper.HashPassword(newpassword, BCryptHelper.GenerateSalt(10));
                        user.HashedPassword = hashed;
                        user.AddLogEntry("Login", "Password was reset");
                        _db.SaveChanges();
                        result.Succesful = true;
                        result.Message = Properties.Resources.PasswordResetSuccedful;
                        result.User = user;
                    }

                    else
                    {
                        result.Message = Properties.Resources.PasswordResetExpired;
                    }
                }

                else
                {
                    result.Message = Properties.Resources.InvalidPassword;
                }
            }

            else
            {
                result.Message = Properties.Resources.PasswordResetTokenInvalid;
            }

            _db.Dispose();
            return result;
        }

        private User GetUserByToken(Guid token)
        {
            return _db.Users.FirstOrDefault(t => t.ReminderToken == token);
        }
    }
}
