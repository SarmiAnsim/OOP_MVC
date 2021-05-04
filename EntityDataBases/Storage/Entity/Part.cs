using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblPart")]
    public class Part : IUniqueldenifiableEntity
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
        [Column("dcCost")]
        [MaxLength(25)]
        public decimal Cost { get; set; }
        [Required]
        [Column("szDescription")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column("gCategoryId")]
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryParts CategoryParts { get; set; }

        [Required]
        [Column("szCategoryName")]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [Column("gCarModelId")]
        public Guid CarModelId { get; set; }
        [ForeignKey(nameof(CarModelId))]
        public CarModel CarModel { get; set; }

        [Required]
        [Column("szCarModelName")]
        [MaxLength(50)]
        public string CarModelName { get; set; }

        [Required]
        [Column("szManufacturerName")]
        [MaxLength(50)]
        public string ManufacturerName { get; set; }
    }
}
