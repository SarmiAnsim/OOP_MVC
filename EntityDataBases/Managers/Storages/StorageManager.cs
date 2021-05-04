using EntityDataBases.Managers.Storages;
using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Storages
{
    public class StorageManager : IStorageManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public StorageManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<Storage.Entity.Storage> AddStorage(CreateOrUpdateStorageRequest request)
        {
            var entity = new Storage.Entity.Storage
            {
                Id = Guid.NewGuid(),
                StorageNumber = request.StorageNumber,
                Address = request.Address,
                CityId = request.CityId,
                CityName = _dbContext.Citys.FirstOrDefault(g => g.Id == request.CityId).Name
            };
            _dbContext.Citys.FirstOrDefault(g => g.Id == request.CityId).NumberOfStorages++;
            _dbContext.Storages.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Storage.Entity.Storage> UpdateStorage(Guid id, Guid Cid, CreateOrUpdateStorageRequest request)
        {
            var entity = await _dbContext.Storages.FirstOrDefaultAsync(g => g.Id == id);

            _dbContext.Citys.FirstOrDefault(g => g.Id == entity.CityId).NumberOfStorages--;
            entity.StorageNumber = request.StorageNumber;
            entity.Address = request.Address;
            entity.CityId = request.CityId;
            entity.CityName = _dbContext.Citys.FirstOrDefault(g => g.Id == Cid).Name;
            _dbContext.Citys.FirstOrDefault(g => g.Id == request.CityId).NumberOfStorages++;

            var orders = _dbContext.Orders.Where(g => g.StorageId == id);
            foreach(var item in orders)
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(g => g.Id == item.Id);
                order.StorageAddress = request.Address;
                order.StorageNumber = request.StorageNumber;
            }
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Storage.Entity.Storage> DeleteStorage(Guid id)
        {
            var entity = await _dbContext.Storages.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            { 
                var querty = _dbContext.Orders.Where(op => op.StorageId == id);
                foreach (var item in querty)
                {
                    var quertys = _dbContext.OrdersParts.Where(op => op.OrderId == entity.Id);
                    foreach (var items in quertys)
                    {
                        _dbContext.OrdersParts.Remove(items);
                    }
                    _dbContext.Orders.Remove(item);
                }
                _dbContext.Storages.Remove(entity);
            }
            var city = await _dbContext.Citys.FirstOrDefaultAsync(g => g.Id == entity.CityId);

            city.NumberOfStorages--;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Storage.Entity.Storage> GetById(Guid id)
        {
            return await _dbContext.Storages.FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IReadOnlyCollection<Storage.Entity.Storage>> GetAllStorages()
        {
            var querty = _dbContext.Storages
                                    .OrderBy(pt => pt.StorageNumber)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<City>> GetAllCitys()
        {
            var querty = _dbContext.Citys
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
    }
}
