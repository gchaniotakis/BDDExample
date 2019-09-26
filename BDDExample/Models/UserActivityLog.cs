using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BDDExample.Models
{
    public class UserActivityLog
    {
        [MaxLength(255)]
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Entry { get; set; }
        public virtual User User { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        

        public UserActivityLog()
        {
            CreatedAt = DateTime.Now;            
        }
    }
}
