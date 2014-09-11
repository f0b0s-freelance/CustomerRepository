using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC.Library
{
    public class ChildDevices
    {
        private string licenseid; // Backing field
        public string LicenseId
        {
            get { return licenseid; }  // Getter
            set { licenseid = value; } // Setter
        }

        private string tbpid; // Backing field
        public string Tbpid
        {
            get { return tbpid; }  // Getter
            set { tbpid = value; } // Setter
        }

        private int childid; // Backing field
        public int ChildId
        {
            get { return childid; }  // Getter
            set { childid = value; } // Setter
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

        private string onlinetimeframes; // Backing field
        public string OnlineTimeFrames
        {
            get { return onlinetimeframes; }  // Getter
            set { onlinetimeframes = value; } // Setter
        }

        private string onlinewebtimeframes; // Backing field
        public string OnlineWebTimeFrames
        {
            get { return onlinewebtimeframes; }  // Getter
            set { onlinewebtimeframes = value; } // Setter
        }

        private string blockedurls; // Backing field
        public string BlockedURLs
        {
            get { return blockedurls; }  // Getter
            set { blockedurls = value; } // Setter
        }

        private string obs; // Backing field
        public string Obs
        {
            get { return obs; }  // Getter
            set { obs = value; } // Setter
        }

        private string type; // Backing field
        public string Type
        {
            get { return type; }  // Getter
            set { type = value; } // Setter
        }
    }
}
