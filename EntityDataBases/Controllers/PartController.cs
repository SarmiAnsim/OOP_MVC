using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.Parts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class PartController : Controller
    {
        private readonly IPartManager _manager;
        public PartController(IPartManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<ViewResult> ShowParts()
        {
            var entity = await _manager.GetAllParts();
            return View(entity);
        }
        #region Create
        public async Task<ViewResult> AddPart()
        {
            ViewBag.CategorysParts = await _manager.GetAllCategorysParts();
            ViewBag.CarsModels = await _manager.GetAllCarsModels();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdatePartRequest request)
        {
            if (request.Name != null && request.Description != null)
            {
                await _manager.AddPart(request);
                return RedirectToAction(nameof(ShowParts));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdatePart(Guid id)
        {
            ViewBag.CategorysParts = await _manager.GetAllCategorysParts();
            ViewBag.CarsModels = await _manager.GetAllCarsModels();
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid PartId, CreateOrUpdatePartRequest request)
        {
            if (request.Name != null && request.Description != null)
            {
                await _manager.UpdatePart(PartId, request);
                return RedirectToAction(nameof(ShowParts));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeletePart(id);
            return RedirectToAction(nameof(ShowParts));
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