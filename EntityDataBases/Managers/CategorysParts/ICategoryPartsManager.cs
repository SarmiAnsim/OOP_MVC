using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CategorysParts
{
    public interface ICategoryPartsManager
    {
        Task<CategoryParts> AddCategoryParts(CreateOrUpdateCategoryRequest request);
        Task<IReadOnlyCollection<CategoryParts>> GetAllCategorysParts();
        Task<CategoryParts> UpdateCategoryParts(Guid id, CreateOrUpdateCategoryRequest request);
        Task<CategoryParts> DeleteCategoryParts(Guid id);
        Task<CategoryParts> GetById(Guid id);
    }
}
