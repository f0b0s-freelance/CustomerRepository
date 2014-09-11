using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC.Library
{
    public class EmailData
    {
        private string fromname; // Backing field
        public string FromName
        {
            get { return fromname; }  // Getter
            set { fromname = value; } // Setter
        }

        private string fromemail; // Backing field
        public string FromEmail
        {
            get { return fromemail; }  // Getter
            set { fromemail = value; } // Setter
        }

        private string toname; // Backing field
        public string ToName
        {
            get { return toname; }  // Getter
            set { toname = value; } // Setter
        }

        private string toemail; // Backing field
        public string ToEmail
        {
            get { return toemail; }  // Getter
            set { toemail = value; } // Setter
        }

        private string subject; // Backing field
        public string Subject
        {
            get { return subject; }  // Getter
            set { subject = value; } // Setter
        }

        private string template; // Backing field
        public string Template
        {
            get { return template; }  // Getter
            set { template = value; } // Setter
        }

        private string text1; // Backing field
        public string Text1
        {
            get { return text1; }  // Getter
            set { text1 = value; } // Setter
        }

        private string text2; // Backing field
        public string Text2
        {
            get { return text2; }  // Getter
            set { text2 = value; } // Setter
        }

        private string text3; // Backing field
        public string Text3
        {
            get { return text3; }  // Getter
            set { text3 = value; } // Setter
        }

        private string text4; // Backing field
        public string Text4
        {
            get { return text4; }  // Getter
            set { text4 = value; } // Setter
        }
    }
}
