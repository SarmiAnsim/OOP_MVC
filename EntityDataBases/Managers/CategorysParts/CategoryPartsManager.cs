using EntityDataBases.Storage;
using EntityDataBases.Storage.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CategorysParts
{
    public class CategoryPartsManager : ICategoryPartsManager
    {
        private readonly AutoPartsStoreDataContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryPartsManager(AutoPartsStoreDataContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<CategoryParts> AddCategoryParts(CreateOrUpdateCategoryRequest request)
        {
            var entity = new CategoryParts
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };
            _dbContext.CategorysParts.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<CategoryParts> UpdateCategoryParts(Guid id, CreateOrUpdateCategoryRequest request)
        {
            var entity = await _dbContext.CategorysParts.FirstOrDefaultAsync(g => g.Id == id);

            var Pentity = _dbContext.Parts.Where(g => g.CategoryId == id);
            foreach(var item in Pentity)
            {
                item.CategoryName = request.Name;
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<CategoryParts> DeleteCategoryParts(Guid id)
        {
            var entity = await _dbContext.CategorysParts.FirstOrDefaultAsync(g => g.Id == id);

            if (entity != null)
            {
                var quert = _dbContext.Parts.Where(op => op.CategoryId == entity.Id);
                foreach(var ite in quert)
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
                _dbContext.CategorysParts.Remove(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<CategoryParts> GetById(Guid id)
        {
            return await _dbContext.CategorysParts.FirstOrDefaultAsync(g => g.Id == id);
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
