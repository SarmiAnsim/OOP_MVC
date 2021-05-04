using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Parts
{
    public interface IPartManager
    {
        Task<Part> AddPart(CreateOrUpdatePartRequest request);
        Task<Part> UpdatePart(Guid id, CreateOrUpdatePartRequest request);
        Task<Part> DeletePart(Guid id);
        Task<Part> GetById(Guid id);
        Task<IReadOnlyCollection<Part>> GetAllParts();
        Task<IReadOnlyCollection<CarModel>> GetAllCarsModels();
        Task<IReadOnlyCollection<CategoryParts>> GetAllCategorysParts();
    }
}
