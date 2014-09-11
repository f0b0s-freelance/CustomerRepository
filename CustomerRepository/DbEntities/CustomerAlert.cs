using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class CustomerAlert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        [Index]
        public int DeviceId { get; set; }
        public string Obs { get; set; }
        public string Msg { get; set; }
        public string Severity { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }

        public virtual Device Device { get; set; }
    }
}
