using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblCarModel")]
    public class CarModel : IUniqueldenifiableEntity
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
        [Column("izYear")]
        public int Year { get; set; }

        [Required]
        [Column("gManufacturerId")]
        public Guid ManufacturerId { get; set; }
        [ForeignKey(nameof(ManufacturerId))]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        [Column("szManufacturerName")]
        [MaxLength(50)]
        public string ManufacturerName { get; set; }
    }
}
