using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Orders
{
    public interface IOrderManager
    {
        Task<Order> AddOrder(CreateOrUpdateOrderRequest request);
        Task<Order> UpdateOrder(Guid id, CreateOrUpdateOrderRequest request);
        Task<Order> ChangePartNumberInOrder(Guid OrderId, Guid PartId, int Num);
        Task<OrdersParts> AddOrdersParts(Guid OrderId, Guid PartId, int Num);
        Task<Order> DeleteOrder(Guid id);
        Task<Order> GetById(Guid id);
        Task<IReadOnlyCollection<Order>> GetAllOrders();
        Task<IReadOnlyCollection<Storage.Entity.Storage>> GetAllStorages();
        Task<IReadOnlyCollection<Part>> GetAllParts();
        Task<IReadOnlyCollection<(Part,int)>> GetParts(Guid id);
    }
}
