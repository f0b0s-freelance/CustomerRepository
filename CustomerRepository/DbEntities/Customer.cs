using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index]
        [MaxLength(50)]
        public string LicenseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Timezone { get; set; }
        public string Hashpass { get; set; }
        public string PaymentProfile { get; set; }
        public DateTime Expires { get; set; }
        public string Obs { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
