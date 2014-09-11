using System;
using System.Collections.Generic;

namespace CustomerRepository.Domain
{
    public class DtoDevice
    {
        public string LicenseId { get; set; }
        public string TbpId { get; set; }
        public IEnumerable<int> ChildIds { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Onlinetimeframes { get; set; }
        public string Onlinewebtimeframes { get; set; }
        public string BlockedUrls { get; set; }
        public string Obs { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public bool Online { get; set; }
    }
}
