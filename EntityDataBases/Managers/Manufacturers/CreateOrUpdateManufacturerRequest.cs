using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Manufacturers
{
    public class CreateOrUpdateManufacturerRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
