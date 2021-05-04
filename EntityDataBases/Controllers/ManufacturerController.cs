using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.Manufacturers;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerManager _manager;
        public ManufacturerController(IManufacturerManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowManufacturers()
        {
            var entity = await _manager.GetAllManufacturers();
            return View(entity);
        }
        #region Create
        public ViewResult AddManufacturer()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateManufacturerRequest request)
        {
            if (request.Name != null)
            {
                await _manager.AddManufacturer(request);
                return RedirectToAction(nameof(ShowManufacturers));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateManufacturer(Guid id)
        {
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid ManufacturerId, CreateOrUpdateManufacturerRequest request)
        {
            if (request.Name != null)
            {
                await _manager.UpdateManufacturer(ManufacturerId, request);
                return RedirectToAction(nameof(ShowManufacturers));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion
        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteManufacturer(id);
            return RedirectToAction(nameof(ShowManufacturers));
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