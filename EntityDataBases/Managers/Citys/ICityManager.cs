using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Citys
{
    public interface ICityManager
    {
        Task<City> AddCity(CreateOrUpdateCityRequest request);
        Task<IReadOnlyCollection<City>> GetAllCitys();
        Task<City> UpdateCity(Guid id, CreateOrUpdateCityRequest request);
        Task<City> DeleteCity(Guid id);
        Task<City> GetById(Guid id);
    }
}
