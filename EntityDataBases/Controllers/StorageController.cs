using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.Storages;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageManager _manager;
        public StorageController(IStorageManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowStorages()
        {
            var entity = await _manager.GetAllStorages();
            return View(entity);
        }
        #region Create
        public async Task<ViewResult> AddStorage()
        {
            var entity = await _manager.GetAllCitys();
            ViewBag.Citys = entity;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateStorageRequest request)
        {
            if (request.Address != null)
            {
                await _manager.AddStorage(request);
                return RedirectToAction(nameof(ShowStorages));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateStorage(Guid id)
        {
            var citys = await _manager.GetAllCitys();
            ViewBag.Citys = citys;
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid StorageId, Guid CityId, CreateOrUpdateStorageRequest request)
        {
            if (request.Address != null)
            {
                await _manager.UpdateStorage(StorageId, CityId, request);
                return RedirectToAction(nameof(ShowStorages));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteStorage(id);
            return RedirectToAction(nameof(ShowStorages));
        }
        #endregion

        #region Error
        public async Task<ViewResult> Error()
        {
            return View();
        }
        #endregion
    }
}