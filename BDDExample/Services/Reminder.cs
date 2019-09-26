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
        public UserMailerMessage
    }

    public class Reminder
    {
    }
}
