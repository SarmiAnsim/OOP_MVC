using EntityDataBases.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.Storages
{
    public interface IStorageManager
    {
        Task<Storage.Entity.Storage> AddStorage(CreateOrUpdateStorageRequest request);
        Task<IReadOnlyCollection<Storage.Entity.Storage>> GetAllStorages();
        Task<IReadOnlyCollection<City>> GetAllCitys();
        Task<Storage.Entity.Storage> UpdateStorage(Guid id, Guid Cid, CreateOrUpdateStorageRequest request);
        Task<Storage.Entity.Storage> DeleteStorage(Guid id);
        Task<Storage.Entity.Storage> GetById(Guid id);
    }
}
