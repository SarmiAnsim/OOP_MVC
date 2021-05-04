using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.CarsModels;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelManager _manager;
        public CarModelController(ICarModelManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowCarsModels(string YearSearch, string ManSearch)
        {
            var entity = await _manager.GetAllCarsModels();
            ViewBag.YearSearch = YearSearch ?? "";
            ViewBag.ManSearch = ManSearch ?? "";
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Search(string YearSearch, string ManSearch)
        {
            return RedirectToAction(nameof(ShowCarsModels), new { YearSearch, ManSearch });
        }
        #region Create
        public async Task<ViewResult> AddCarModel()
        {
            var entity = await _manager.GetAllManufacturers();
            ViewBag.Manufacturers = entity;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateCarModelRequest request)
        {
            if (request.Name != null)
            {
                await _manager.AddCarModel(request);
                return RedirectToAction(nameof(ShowCarsModels));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateCarModel(Guid id)
        {
            var manufacturers = await _manager.GetAllManufacturers();
            ViewBag.Manufacturers = manufacturers;
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid CarModelId, Guid ManufacturerId, CreateOrUpdateCarModelRequest request)
        {
            if (request.Name != null)
            {
                await _manager.UpdateCarModel(CarModelId, ManufacturerId, request);
                return RedirectToAction(nameof(ShowCarsModels));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteCarModel(id);
            return RedirectToAction(nameof(ShowCarsModels));
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