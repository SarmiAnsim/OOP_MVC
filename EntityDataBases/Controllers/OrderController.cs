using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _manager;
        public OrderController(IOrderManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<ViewResult> ShowOrders(string ClientSearch, string StorageSearch)
        {
            var entity = await _manager.GetAllOrders();
            ViewBag.ClientSearch = ClientSearch ?? "";
            ViewBag.StorageSearch = StorageSearch ?? "";
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Search(string ClientSearch, string StorageSearch)
        {
            return RedirectToAction(nameof(ShowOrders), new { ClientSearch, StorageSearch });
        }
        public async Task<ViewResult> ShowOrder(Guid id)
        {
            var entity = await _manager.GetParts(id);
            var order = await _manager.GetById(id);
            ViewBag.Order = order;
            return View(entity);
        }
        #region Create
        public async Task<ViewResult> AddOrder()
        {
            var entity = await _manager.GetAllStorages();
            ViewBag.Storages = entity;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateOrderRequest request)
        {
            if (request.Client != null)
            {
                await _manager.AddOrder(request);
                return RedirectToAction(nameof(ShowOrders));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        public async Task<ViewResult> AddPartInOrder(Guid OrderId)
        {
            var entity = await _manager.GetAllParts();
            ViewBag.Parts = entity;
            ViewBag.OrderId = OrderId;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Guid OrderId,Guid PartId, int Num)
        {
            await _manager.AddOrdersParts(OrderId, PartId, Num);
            return RedirectToAction(nameof(ShowOrder), new {id = OrderId});
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<ViewResult> UpdateOrder(Guid id)
        {
            var storages = await _manager.GetAllStorages();
            ViewBag.Storages = storages;
            var entity = await _manager.GetById(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<ActionResult> Update(Guid OrderId, CreateOrUpdateOrderRequest request)
        {
            if (request.Client != null)
            {
                await _manager.UpdateOrder(OrderId, request);
                return RedirectToAction(nameof(ShowOrders));
            }
            else
                return RedirectToAction(nameof(Error));
        }
        [HttpPost]
        public async Task<ActionResult> ChangePNumber(Guid Orderid, Guid PartId, int Num)
        {
            await _manager.ChangePartNumberInOrder(Orderid, PartId, Num);
            return RedirectToAction(nameof(ShowOrder), new { id = Orderid });
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            await _manager.DeleteOrder(id);
            return RedirectToAction(nameof(ShowOrders));
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