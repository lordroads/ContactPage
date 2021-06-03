using ContactPage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Test = "TEST"; //@Html.Raw(ViewBag.Test)
            return View();
        }

        public IActionResult Archive()
        {
            ViewData["Message"] = "Задача архива.";

            return View();
        }
        public IActionResult NetworkOffice() => View();

        public IActionResult Documents()
        {
            ViewData["Message"] = "Тут надо сделать список документов компании!";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
