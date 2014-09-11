using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPCServices.Entities
{
    public class TicketData
    {
        private string licenseid; // Backing field
        public string LicenseId
        {
            get { return licenseid; }  // Getter
            set { licenseid = value; } // Setter
        }

        private string message; // Backing field
        public string Message
        {
            get { return message; }  // Getter
            set { message = value; } // Setter
        }

        private string email; // Backing field
        public string Email
        {
            get { return email; }  // Getter
            set { email = value; } // Setter
        }
    }
}