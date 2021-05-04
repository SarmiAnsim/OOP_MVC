using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EntityDataBases.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}