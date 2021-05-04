using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblCity")]
    public class City : IUniqueldenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("szName")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("izNumberOfWarehouses")]
        public int NumberOfStorages { get; set; }

    }
}
