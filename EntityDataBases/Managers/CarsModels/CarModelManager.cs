using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CarsModels
{
    public class CarModelManager : ICarModelManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CarModelManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<CarModel> AddCarModel(CreateOrUpdateCarModelRequest request)
        {
            var entity = new CarModel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Year = request.Year,
                ManufacturerId = request.ManufacturerId,
                ManufacturerName = _dbContext.Manufacturers.FirstOrDefault(g => g.Id == request.ManufacturerId).Name
            };
            _dbContext.CarsModels.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<CarModel> UpdateCarModel(Guid id, Guid Mid, CreateOrUpdateCarModelRequest request)
        {
            var entity = await _dbContext.CarsModels.FirstOrDefaultAsync(g => g.Id == id);

            var parts = _dbContext.Parts.Where(g => g.CarModelName == entity.Name);
            foreach (var item in parts)
            {
                var part = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == item.Id);
                part.CarModelName = request.Name;
            }
            entity.Name = request.Name;
            entity.Year = request.Year;
            entity.ManufacturerId = request.ManufacturerId;
            entity.ManufacturerName = _dbContext.Manufacturers.FirstOrDefault(g => g.Id == Mid).Name;


            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<CarModel> DeleteCarModel(Guid id)
        {
            var entity = await _dbContext.CarsModels.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                var quert = _dbContext.Parts.Where(op => op.CarModelId == entity.Id);
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
                _dbContext.CarsModels.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<CarModel> GetById(Guid id)
        {
            return await _dbContext.CarsModels.FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IReadOnlyCollection<CarModel>> GetAllCarsModels()
        {
            var querty = _dbContext.CarsModels
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
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
