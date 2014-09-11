using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class ChildDevice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ChildId { get; set; }
        public int DeviceId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Child Child { get; set; }
    }
}
