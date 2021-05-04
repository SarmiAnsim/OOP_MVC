using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage.Entity
{
    [Table("tblStorage")]
    public class Storage : IUniqueldenifiableEntity
    {
        [Key]
        [Required]
        [Column("gId")]
        public Guid Id { get; set; }
        [Required]
        [Column("izStorageNumber")]
        public int StorageNumber { get; set; }
        [Required]
        [Column("szAddress")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [Column("gCityId")]
        public Guid CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [Required]
        [Column("szCityName")]
        [MaxLength(50)]
        public string CityName { get; set; }
    }
}
