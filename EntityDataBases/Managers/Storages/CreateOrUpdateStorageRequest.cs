using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Storages
{
    public class CreateOrUpdateStorageRequest
    {
        public int StorageNumber { get; set; }
        public string Address { get; set; }
        public Guid CityId { get; set; }
    }
}
