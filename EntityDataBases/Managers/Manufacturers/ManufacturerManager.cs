using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Manufacturers
{
    public class ManufacturerManager : IManufacturerManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ManufacturerManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<Manufacturer> AddManufacturer(CreateOrUpdateManufacturerRequest request)
        {
            var entity = new Manufacturer
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Country = request.Country
            };
            _dbContext.Manufacturers.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Manufacturer> UpdateManufacturer(Guid id, CreateOrUpdateManufacturerRequest request)
        {
            var entity = await _dbContext.Manufacturers.FirstOrDefaultAsync(g => g.Id == id);
            
            var CMentity = _dbContext.CarsModels.Where(g => g.ManufacturerId == id);
            foreach(var item in CMentity)
            {
                item.ManufacturerName = request.Name;
                var Pentity = _dbContext.Parts.Where(g => g.CarModelName == item.Name);
                foreach(var it in Pentity)
                {
                    it.ManufacturerName = request.Name;
                }
            }

            entity.Name = request.Name;
            entity.Country = request.Country;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Manufacturer> DeleteManufacturer(Guid id)
        {
            var entity = await _dbContext.Manufacturers.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                var quer = _dbContext.CarsModels.Where(op => op.ManufacturerId == entity.Id);
                foreach (var it in quer)
                { 
                    var quert = _dbContext.Parts.Where(op => op.CarModelId == it.Id);
                    foreach (var ite in quert)
                    {
                        var querty = _dbContext.OrdersParts.Where(op => op.PartId == ite.Id);
                        foreach (var item in querty)
                        {
                            var quertys = await _dbContext.Orders.FirstOrDefaultAsync(op => op.Id == item.OrderId);

                            quertys.Cost -= item.NumberPartInOrder * ite.Cost;
                            _dbContext.OrdersParts.Remove(item);
                        }
                        _dbContext.Parts.Remove(ite);
                    }
                    _dbContext.CarsModels.Remove(it);
                }
                _dbContext.Manufacturers.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Manufacturer> GetById(Guid id)
        {
            return await _dbContext.Manufacturers.FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IReadOnlyCollection<Manufacturer>> GetAllManufacturers()
        {
            var querty = _dbContext.Manufacturers
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
    }
}
