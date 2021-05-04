using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Citys
{
    public class CityManager : ICityManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CityManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<City> AddCity(CreateOrUpdateCityRequest request)
        {
            var entity = new City
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                NumberOfStorages = 0
            };
            _dbContext.Citys.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<City> UpdateCity(Guid id, CreateOrUpdateCityRequest request)
        {
            var entity = await _dbContext.Citys.FirstOrDefaultAsync(g => g.Id == id);
            var storages = _dbContext.Storages.Where(g => g.CityId == entity.Id);
            foreach(var item in storages)
            {
                var storage = await _dbContext.Storages.FirstOrDefaultAsync(g => g.CityName == entity.Name);
                storage.CityName = request.Name;
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<City> DeleteCity(Guid id)
        {
            var entity = await _dbContext.Citys.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                var querty = _dbContext.Storages.Where(op => op.CityId == id);
                foreach (var item in querty)
                {
                    var quertys = _dbContext.Orders.Where(op => op.StorageId == item.Id);
                    foreach (var items in quertys)
                    {
                        var quertyss = _dbContext.OrdersParts.Where(op => op.OrderId == items.Id);
                        foreach(var itemss in quertyss)
                        {
                            _dbContext.OrdersParts.Remove(itemss);
                        }
                        _dbContext.Orders.Remove(items);
                    }
                    _dbContext.Storages.Remove(item);
                }
                _dbContext.Citys.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<City> GetById(Guid id)
        {
            return await _dbContext.Citys.FirstOrDefaultAsync(g => g.Id == id);
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
