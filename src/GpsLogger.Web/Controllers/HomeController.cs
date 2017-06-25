using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpsLogger.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GpsLogger.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
