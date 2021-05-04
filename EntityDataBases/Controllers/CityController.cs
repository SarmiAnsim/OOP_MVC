using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.Citys;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityManager _manager;
        public CityController(ICityManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowCitys()
        {
            var entity = await _manager.GetAllCitys();
            return View(entity);
        }
        #region Create
        public ViewResult AddCity()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateCityRequest request)
        {
            if(request.Name != null)
            { 
                await _manager.AddCity(request);
                return RedirectToAction(nameof(ShowCitys));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateCity(Guid id)
        {
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid CityId, CreateOrUpdateCityRequest request)
        {
            if (request.Name != null)
            {
                await _manager.UpdateCity(CityId, request);
                return RedirectToAction(nameof(ShowCitys), new { CityId });
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteCity(id);
            return RedirectToAction(nameof(ShowCitys));
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