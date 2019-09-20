using System;
using System.Collections.Generic;
using System.Text;

namespace BDDExample.Models
{
    public class Application
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public ApplicationStatus Status { get; set; }
        public string UserMessage { get; set; }

        public Application(string email, string password, string confirm)
        {
            Email = email;
            Password = password;
            Confirmation = confirm;           
            Status = ApplicationStatus.NotProcessed;
        }

        public bool IsAccpeted()
        {
            return Status == ApplicationStatus.Accepted;
        }
        public bool IsInvalid()
        {
            return !IsValid();
        }

        public bool IsValid()
        {
            return Status == ApplicationStatus.Validated ||
                Status == ApplicationStatus.Accepted;
        }
    }

    public enum ApplicationStatus
    {
        Pending,
        Denied,
        Accepted,
        Invalid, 
        NotProcessed,
        Validated
    }
}
