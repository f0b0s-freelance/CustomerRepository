using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPCServices.ExceptionHandling
{
    //{
    //    "hashKey": "aksdagsdghkjweyutuwie78wh",
    //    "licenseid": "ahjsdfasdfjhasjdfjasjdf",
    //    "environment": "Development|Production",
    //    "type": "Windows|Mobile|Service",
    //    "remoteip": "127.0.0.1",
    //    "exception": "Object not defined as an instance of object",
    //    "osname": "Windows 8.1",
    //    "osedition": "Home",
    //    "sp": "SP1",
    //    "version": "6.3.2.1",
    //    "processor": "64b",
    //    "osbits": "32b",
    //    "spcappversion": "1.0.1"
    //    "browser": "IE"
    //    "browserversion": "7"    
    //}
    public class ExceptionObj
    {

        private string id; // Backing field
        public string Id
        {
            get { return id; }  // Getter
            set { id = value; } // Setter
        }

        private string licenseid; // Backing field
        public string LicenseId
        {
            get { return licenseid; }  // Getter
            set { licenseid = value; } // Setter
        }

        private string environment; // Backing field
        public string Environment
        {
            get { return environment; }  // Getter
            set { environment = value; } // Setter
        }

        private string type; // Backing field
        public string Type
        {
            get { return type; }  // Getter
            set { type = value; } // Setter
        }

        private string remoteip; // Backing field
        public string remoteIP
        {
            get { return remoteip; }  // Getter
            set { remoteip = value; } // Setter
        }

        private string exception; // Backing field
        public string Exception
        {
            get { return exception; }  // Getter
            set { exception = value; } // Setter
        }

        private string osname; // Backing field
        public string OsName
        {
            get { return osname; }  // Getter
            set { osname = value; } // Setter
        }

        private string osedition; // Backing field
        public string OsEdition
        {
            get { return osedition; }  // Getter
            set { osedition = value; } // Setter
        }

        private string sp; // Backing field
        public string SP
        {
            get { return sp; }  // Getter
            set { sp = value; } // Setter
        }

        private string version; // Backing field
        public string Version
        {
            get { return version; }  // Getter
            set { version = value; } // Setter
        }

        private string processor; // Backing field
        public string Processor
        {
            get { return processor; }  // Getter
            set { processor = value; } // Setter
        }

        private string osbits; // Backing field
        public string OSBits
        {
            get { return osbits; }  // Getter
            set { osbits = value; } // Setter
        }

        private string spcappversion; // Backing field
        public string SPCAppVersion
        {
            get { return spcappversion; }  // Getter
            set { spcappversion = value; } // Setter
        }

        private string browser; // Backing field
        public string Browser
        {
            get { return browser; }  // Getter
            set { browser = value; } // Setter
        }

        private string browserversion; // Backing field
        public string BrowserVersion
        {
            get { return browserversion; }  // Getter
            set { browserversion = value; } // Setter
        }
    }
}