using System;
using System.Collections.Generic;
using System.Text;

namespace BDDExample.Models
{
    public class Application
    {
        public bool IsValid { get; set; }
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
            IsValid = false;
            Status = ApplicationStatus.Pending;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                throw new InvalidOperationException("Can't submit an application without an email or password");
            }
        }
    }

    public enum ApplicationStatus
    {
        Pending,
        Denied,
        Accepted,
    }
}
