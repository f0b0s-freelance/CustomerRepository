using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC.Library
{
    public class ChildData
    {
        private string licenseid; // Backing field
        public string LicenseId
        {
            get { return licenseid; }  // Getter
            set { licenseid = value; } // Setter
        }

        private int id; // Backing field
        public int Id
        {
            get { return id; }  // Getter
            set { id = value; } // Setter
        }

        private string name; // Backing field
        public string Name
        {
            get { return name; }  // Getter
            set { name = value; } // Setter
        }

        private string avatar; // Backing field
        public string Avatar
        {
            get { return avatar; }  // Getter
            set { avatar = value; } // Setter
        }

        private string obs; // Backing field
        public string Obs
        {
            get { return obs; }  // Getter
            set { obs = value; } // Setter
        }

        private ChildDevices devices; // Backing field
        public ChildDevices Devices
        {
            get { return devices; }  // Getter
            set { devices = value; } // Setter
        }

        private string category; // Backing field
        public string Category
        {
            get { return category; }  // Getter
            set { category = value; } // Setter
        }
 
    }
}
