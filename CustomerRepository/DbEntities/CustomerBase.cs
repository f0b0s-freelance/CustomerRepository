using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class CustomerBase
    {
        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        [Index]
        public int ChildId { get; set; }
        [Index]
        public int DeviceId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }
    }
}