using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        public string TbpId { get; set; }
        //public int ChildId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string OnlineTimeFrames { get; set; }
        public string OnlineWebTimeFrames { get; set; }
        public string BlockedUrls { get; set; }
        public string Obs { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ChildDevice> ChildDevices { get; set; }
    }
}
