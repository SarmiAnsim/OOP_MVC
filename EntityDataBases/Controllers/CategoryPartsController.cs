using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.CategorysParts;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class CategoryPartsController : Controller
    {
        private readonly ICategoryPartsManager _manager;
        public CategoryPartsController(ICategoryPartsManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowCategorysParts()
        {
            var entity = await _manager.GetAllCategorysParts();
            return View(entity);
        }
        #region Create
        public ViewResult AddCategoryParts()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateCategoryRequest request)
        {
            if (request.Name != null && request.Description != null)
            {
                await _manager.AddCategoryParts(request);
                return RedirectToAction(nameof(ShowCategorysParts));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateCategoryParts(Guid id)
        {
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid CategoryPartsId, CreateOrUpdateCategoryRequest request)
        {
            if (request.Name != null && request.Description != null)
            {
                await _manager.UpdateCategoryParts(CategoryPartsId, request);
                return RedirectToAction(nameof(ShowCategorysParts));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteCategoryParts(id);
            return RedirectToAction(nameof(ShowCategorysParts));
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