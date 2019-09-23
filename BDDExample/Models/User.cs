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
            MailerLogs = new List<UserMailerLog>();
            CreatedAt = DateTime.Now;
            Sessions = new List<UserSession>();
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
        public ICollection<UserMailerLog> MailerLogs { get; set; }
        public ICollection<UserSession> Sessions { get; set; }
        public DateTime CreatedAt { get; set; }

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
        Pending = 1
    }
}
