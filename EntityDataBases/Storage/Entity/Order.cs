using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblOrder")]
    public class Order : IUniqueldenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }

        [Required]
        [Column("izOrderNumber")]
        public int OrderNumber { get; set; }

        [Required]
        [Column("dcCost")]
        [MaxLength(25)]
        public decimal Cost { get; set; }

        [Required]
        [Column("dtOrderTime")]
        public DateTime OrderTime { get; set; }

        [Required]
        [Column("szClient")]
        [MaxLength(50)]
        public string Client { get; set; }

        [Required]
        [Column("gStorageId")]
        public Guid StorageId { get; set; }
        [ForeignKey(nameof(StorageId))]
        public Storage Storage { get; set; }

        [Required]
        [Column("izStorageStorageNumber")]
        [MaxLength(50)]
        public int StorageNumber { get; set; }

        [Required]
        [Column("szStorageAddress")]
        [MaxLength(100)]
        public string StorageAddress { get; set; }
    }
}
