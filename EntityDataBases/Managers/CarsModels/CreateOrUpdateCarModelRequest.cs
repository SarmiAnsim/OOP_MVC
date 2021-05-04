using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CarsModels
{
    public class CreateOrUpdateCarModelRequest
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
