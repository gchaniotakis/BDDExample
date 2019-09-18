using System;
using System.Collections.Generic;
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
        }

        public UserStatus Status;
        public Guid Id { get; set; }
        public ICollection<UserActivityLog> Logs { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserMailerLog> MailerLogs { get; set; }
    }

    public enum UserStatus
    {
        Pending
    }
}
