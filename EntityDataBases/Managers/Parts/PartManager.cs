using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Parts
{
    public class PartManager : IPartManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PartManager (AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Part> AddPart(CreateOrUpdatePartRequest request)
        {
            var entity = new Part
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Cost = request.Cost,
                Description = request.Description,
                CategoryId = request.CategoryId,
                CarModelId = request.CarModelId,
                CarModelName = _dbContext.CarsModels.FirstOrDefault(g => g.Id == request.CarModelId).Name,
                CategoryName = _dbContext.CategorysParts.FirstOrDefault(g => g.Id == request.CategoryId).Name,
                ManufacturerName = _dbContext.CarsModels.FirstOrDefault(g => g.Id == request.CarModelId).ManufacturerName
            };
            _dbContext.Parts.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Part> UpdatePart(Guid id, CreateOrUpdatePartRequest request)
        {
            var entity = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == id);

            entity.Name = request.Name;
            entity.Cost = request.Cost;
            entity.Description = request.Description;
            entity.CategoryId = request.CategoryId;
            entity.CarModelId = request.CarModelId;
            entity.CarModelName = _dbContext.CarsModels.FirstOrDefault(g => g.Id == request.CarModelId).Name;
            entity.CategoryName = _dbContext.CategorysParts.FirstOrDefault(g => g.Id == request.CategoryId).Name;
            entity.ManufacturerName = _dbContext.CarsModels.FirstOrDefault(g => g.Id == request.CarModelId).ManufacturerName;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Part> DeletePart(Guid id)
        {
            var entity = await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                var querty = _dbContext.OrdersParts.Where(op => op.PartId == entity.Id);
                foreach (var item in querty)
                {
                    var quertys = await _dbContext.Orders.FirstOrDefaultAsync(op => op.Id == item.OrderId);

                    quertys.Cost -= item.NumberPartInOrder * entity.Cost;
                    _dbContext.OrdersParts.Remove(item);
                }
                _dbContext.Parts.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<Part> GetById(Guid id)
        {
            return await _dbContext.Parts.FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IReadOnlyCollection<Part>> GetAllParts()
        {
            var querty = _dbContext.Parts
                                    .Include(pt => pt.CategoryParts)
                                    .Include(pt => pt.CarModel)
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<CarModel>> GetAllCarsModels()
        {
            var querty = _dbContext.CarsModels
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
        public async Task<IReadOnlyCollection<CategoryParts>> GetAllCategorysParts()
        {
            var querty = _dbContext.CategorysParts
                                    .OrderBy(pt => pt.Name)
                                    .AsNoTracking();
            var entityes = await querty.ToListAsync();
            return entityes;
        }
    }
}
