using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(500)]
        [Required]
        public string HashedPassword { get; set; }
        public Guid Id { get; set; }
        public UserStatus Status;
        public ICollection<UserActivityLog> Logs { get; set; }
        public ICollection<UserMailerLog> MailerLogs { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum UserStatus
    {
        Pending
    }
}
