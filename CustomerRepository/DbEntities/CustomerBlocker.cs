﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRepository.DbEntities
{
    public class CustomerBlocker : CustomerBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Obs { get; set; }
        public string Blocker { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Child Child { get; set; }
        public virtual Device Device { get; set; }
    }
}
