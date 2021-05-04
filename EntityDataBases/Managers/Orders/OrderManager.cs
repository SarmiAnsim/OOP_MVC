using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public OrderManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<Order> AddOrder(CreateOrUpdateOrderRequest request)
        {
            var entity = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = request.OrderNumber,
                Cost = 0,
                OrderTime = request.OrderTime,
                Client = request.Client,
                StorageId = request.StorageId,
                StorageNumber = _dbContext.Storages.FirstOrDefault(g => g.Id == request.StorageId).StorageNumber,
                StorageAddress = _dbContext.Storages.FirstOrDefault(g => g.Id == request.StorageId).Address
            };
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Order> UpdateOrder(Guid id, CreateOrUpdateOrderRequest request)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == id);

            entity.OrderNumber = request.OrderNumber;
            entity.OrderTime = request.OrderTime;
            entity.Client = request.Client;
            entity.StorageId = request.StorageId;
            entity.StorageNumber = _dbContext.Storages.FirstOrDefault(g => g.Id == request.StorageId).StorageNumber;
            entity.StorageAddress = _dbContext.Storages.FirstOrDefault(g => g.Id == request.StorageId).Address;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Order> ChangePartNumberInOrder(Guid OrderId, Guid PartId, int Num)
        {
            var entity = await _dbContext.OrdersParts.FirstOrDefaultAsync(g => g.OrderId == OrderId && g.PartId == PartId);
           
            entity.NumberPartInOrder += Num;
            if (entity.NumberPartInOrder == 0)
                _dbContext.OrdersParts.Remove(entity);

            var order = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == OrderId);
            var part = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == PartId);
            order.Cost += Num * part.Cost;
            await _dbContext.SaveChangesAsync();

            return order;
        }
        public async Task<OrdersParts> AddOrdersParts(Guid OrderId, Guid PartId, int Num)
        {
            var entity = new OrdersParts
            {
                Id = Guid.NewGuid(),
                OrderId = OrderId,
                PartId = PartId,
                NumberPartInOrder = Num
            };
            var ordersparts = await _dbContext.OrdersParts.FirstOrDefaultAsync(g => g.OrderId == OrderId && g.PartId == PartId);
            if (ordersparts == null)
                _dbContext.OrdersParts.Add(entity);
            else
                ordersparts.NumberPartInOrder += entity.NumberPartInOrder;

            var orderentity = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == OrderId);
            var partentity = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == PartId);
            orderentity.Cost += partentity.Cost * Num;

            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Order> DeleteOrder(Guid id)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                _dbContext.Orders.Remove(entity);
                var querty = _dbContext.OrdersParts.Where(op => op.OrderId == entity.Id);
                foreach (var item in querty)
                {
                    _dbContext.OrdersParts.Remove(item);
                }
            }
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Order> GetById(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IReadOnlyCollection<Order>> GetAllOrders()
        {
            var querty = _dbContext.Orders
                                    .OrderBy(pt => pt.OrderTime)
                                    .AsNoTracking();
            foreach(var item in querty)
            {
                decimal Cost = 0;
                var OP = _dbContext.OrdersParts.Where(g => g.OrderId == item.Id);
                foreach (var it in OP)
                {
                    var part = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == it.PartId);
                    Cost += part.Cost * it.NumberPartInOrder;
                }
                var order = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == item.Id);
                order.Cost = Cost;
            }

            await _dbContext.SaveChangesAsync();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<Storage.Entity.Storage>> GetAllStorages()
        {
            var querty = _dbContext.Storages
                                    .OrderBy(pt => pt.StorageNumber)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<Part>> GetAllParts()
        {
            var querty = _dbContext.Parts
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<(Part,int)>> GetParts(Guid id)
        {
            var querty = _dbContext.OrdersParts.Where(op => op.OrderId == id);
            List<(Part, int)> parts = new List<(Part, int)>();
            foreach(var item in querty)
            {
                parts.Add((_dbContext.Parts.FirstOrDefault(pt => pt.Id == item.PartId), item.NumberPartInOrder));
            }
            return parts;
        }
    }
}
