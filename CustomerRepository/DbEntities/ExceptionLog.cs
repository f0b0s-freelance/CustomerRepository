using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    class ExceptionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        public string Environment { get; set; }
        public string Type { get; set; }
        public string RemoteIp { get; set; }
        public string Exception { get; set; }
        public string OsName { get; set; }
        public string OsEdition { get; set; }
        public string Sp { get; set; }
        public string Processor { get; set; }
        public string Osbits { get; set; }
        public string SpcAppVersion { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }
    }
}
