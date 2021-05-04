using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Orders
{
    public class CreateOrUpdateOrderRequest
    {
        public int OrderNumber { get; set; }
        public decimal Cost { get; set; }
        public DateTime OrderTime { get; set; }
        public string Client { get; set; }
        public Guid StorageId { get; set; }
    }
}
