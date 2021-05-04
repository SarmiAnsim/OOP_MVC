using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Parts
{
    public class CreateOrUpdatePartRequest
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CarModelId { get; set; }
    }
}
