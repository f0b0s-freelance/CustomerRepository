using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC.Library
{
    public class CustomerData
    {

        private string licenseid; // Backing field
        public string LicenseId
        {
            get { return licenseid; }  // Getter
            set { licenseid = value; } // Setter
        }

        private string firstname; // Backing field
        public string FirstName
        {
            get { return firstname; }  // Getter
            set { firstname = value; } // Setter
        }

        private string lasttname; // Backing field
        public string LastName
        {
            get { return lasttname; }  // Getter
            set { lasttname = value; } // Setter
        }

        private string dateofbirth; // Backing field
        public string DateOfBirth
        {
            get { return dateofbirth; }  // Getter
            set { dateofbirth = value; } // Setter
        }

        private string email; // Backing field
        public string Email
        {
            get { return email; }  // Getter
            set { email = value; } // Setter
        }

        private bool isemailverified; // Backing field
        public bool IsEmailVerified
        {
            get { return isemailverified; }  // Getter
            set { isemailverified = value; } // Setter
        }

        private string timezone; // Backing field
        public string Timezone
        {
            get { return timezone; }  // Getter
            set { timezone = value; } // Setter
        }

        private string hashpass; // Backing field
        public string HashPass
        {
            get { return hashpass; }  // Getter
            set { hashpass = value; } // Setter
        }

        private string paymentprofile; // Backing field
        public string PaymentProfile
        {
            get { return paymentprofile; }  // Getter
            set { paymentprofile = value; } // Setter
        }

        private DateTime expires; // Backing field
        public DateTime Expires
        {
            get { return expires; }  // Getter
            set { expires = value; } // Setter
        }

        private string obs; // Backing field
        public string Obs
        {
            get { return obs; }  // Getter
            set { obs = value; } // Setter
        }

        private string status; 
        public string Status
        {
            get { return status; }  // Getter
            set { status = value; } // Setter
        }

        private string type; 
        public string Type
        {
            get { return type; }  
            set { type = value; } 
        }

        private string paymenturl;
        public string PaymentURL
        {
            get { return paymenturl; }
            set { paymenturl = value; }
        }

        private ChildData childs; // Backing field
        public ChildData Child
        {
            get { return childs; }  // Getter
            set { childs = value; } // Setter
        }

    }
}
