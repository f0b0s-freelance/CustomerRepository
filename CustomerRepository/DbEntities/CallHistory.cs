using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class CallHistory : CustomerBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Obs { get; set; }
        public string Number { get; set; }
        public string Duration { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Child Child { get; set; }
        public virtual Device Device { get; set; }
    }
}
