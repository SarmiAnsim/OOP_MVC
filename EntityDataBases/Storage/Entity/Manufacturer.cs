using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblManufacturer")]
    public class Manufacturer : IUniqueldenifiableEntity
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
        [Column("szCountry")]
        [MaxLength(50)]
        public string Country { get; set; }
    }
}
