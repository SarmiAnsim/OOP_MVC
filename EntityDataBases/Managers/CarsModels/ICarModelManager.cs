using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CarsModels
{
    public interface ICarModelManager
    {
        Task<CarModel> AddCarModel(CreateOrUpdateCarModelRequest request);
        Task<IReadOnlyCollection<CarModel>> GetAllCarsModels();
        Task<IReadOnlyCollection<Manufacturer>> GetAllManufacturers();
        Task<CarModel> UpdateCarModel(Guid id, Guid Mid, CreateOrUpdateCarModelRequest request);
        Task<CarModel> DeleteCarModel(Guid id);
        Task<CarModel> GetById(Guid id);
    }
}
