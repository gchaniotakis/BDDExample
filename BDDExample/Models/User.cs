using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BDDExample.Models
{
    public class User
    {
        public User()
        {
            Status = UserStatus.Pending;
            Id = Guid.NewGuid();
            Logs = new List<UserActivityLog>();
            MailerLogs = new List<UserMailerMessage>();
            CreatedAt = DateTime.Now;
            Sessions = new List<UserSession>();
            LastSignInAt = DateTime.Now;
            CurrentSignInAt = DateTime.Now;
            SignInCount = 0;
            AuthenticationToken = Guid.NewGuid();
            ReminderToken = Guid.NewGuid();
        }

        [MaxLength(255)]
        [Required]
        public string Email { get; set; }
        [MaxLength(500)]
        [Required]
        public string HashedPassword { get; set; }
        public Guid Id { get; set; }
        [Required]
        public UserStatus Status;
        public ICollection<UserActivityLog> Logs { get; set; }
        public ICollection<UserMailerMessage> MailerLogs { get; set; }
        public ICollection<UserSession> Sessions { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public Guid AuthenticationToken { get; set; }
        public string IP { get; set; }
        public DateTime LastSignInAt { get; set; }
        public DateTime CurrentSignInAt { get; set; }
        [Required]
        public int SignInCount { get; set; }
        public Guid ReminderToken { get; set; }
        public DateTime? reminderSentAt { get; set; }


        public void AddLogEntry(string subject, string entry)
        {
            Logs.Add(new UserActivityLog { Subject = subject, Entry = entry });
        }

        public UserSession CurrentSession {
            get
            {
                return Sessions.OrderByDescending(x => x.FinishedAt).FirstOrDefault();
            }
        }
    }

    public enum UserStatus
    {
        Pending = 1,
        InvalidEmail = 66
    }
}
