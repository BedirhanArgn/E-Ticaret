using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;

namespace E_Ticaret.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
        
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();

        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            return RedirectToAction("list");
        }

        [HttpPost]
        public IActionResult Delete(int productid)
        {
            return RedirectToAction("list");
        }


        public IActionResult About()
        {
            return View();
        }
    }

}
