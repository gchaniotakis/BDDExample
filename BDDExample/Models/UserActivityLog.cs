using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BDDExample.Models
{
    public class UserActivityLog
    {
        [MaxLength(255)]
        public string Subject { get; set; }
        public string Entry { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }

        public UserActivityLog()
        {
            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }
}
