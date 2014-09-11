using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC.Library
{
    public class URLsToBlock
    {
        private List<string> urllist; // Backing field
        public List<string> UrlList
        {
            get { return urllist; }  // Getter
            set { urllist = value; } // Setter
        }
    }
}