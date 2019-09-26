using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Net.Mail;

namespace BDDExample.Models
{
    public class UserMailerMessage
    {
        public int Id { get; set; }
        [Required]
        public bool Succesful { get; set; }
        [Required]
        [MaxLength(255)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public virtual UserMailerTemplate Mailer { get; set; }
        public string ResultMessage { get; set; }

        public UserMailerMessage()
        {
            CreatedAt = DateTime.Now;            
        }

        public UserMailerMessage SendTo(User user)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = true;
            Succesful = false;

            SmtpClient client = new SmtpClient();
            try
            {
                client.Send(message);
                Succesful = true;
            }

            catch (SmtpFailedRecipientException e)
            {
                ResultMessage = e.Message;
                user.Status = UserStatus.InvalidEmail;
            }
            catch(SmtpException e)
            {
                ResultMessage = e.Message;
            }

            user.MailerLogs.Add(this);

            return this;
        }
    }
}
