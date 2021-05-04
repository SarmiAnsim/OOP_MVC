using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Manufacturers
{
    public interface IManufacturerManager
    {
        Task<Manufacturer> AddManufacturer(CreateOrUpdateManufacturerRequest request);
        Task<IReadOnlyCollection<Manufacturer>> GetAllManufacturers();
        Task<Manufacturer> UpdateManufacturer(Guid id, CreateOrUpdateManufacturerRequest request);
        Task<Manufacturer> DeleteManufacturer(Guid id);
        Task<Manufacturer> GetById(Guid id);
    }
}
