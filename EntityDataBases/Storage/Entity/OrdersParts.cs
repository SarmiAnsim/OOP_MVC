using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblOrdersParts")]
    public class OrdersParts
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }

        [Required]
        [Column("gOrderId")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("gPartId")]
        public Guid PartId { get; set; }

        [Required]
        [Column("izNumberInOrder")]
        public int NumberPartInOrder { get; set; }
    }
}
