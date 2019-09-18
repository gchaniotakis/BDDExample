using System;
using System.Collections.Generic;
using System.Text;

namespace BDDExample.Models
{
    public class Application
    {
        public bool HasBeenValidated { get; set; }

        public Application()
        {
            HasBeenValidated = false;
        }
    }

    public enum ApplicationStatus
    {
        Pending,
        Validated,
        Accepted,
    }
}
