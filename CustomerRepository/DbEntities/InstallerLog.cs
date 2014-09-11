using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    internal class InstallerLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public string RemoteIp { get; set; }
        public string Step { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }
    }
}
