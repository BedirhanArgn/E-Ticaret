using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.data.Abstract;
using E_Ticaret.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productservice;
        public HomeController(IProductService productser) //Depedency Injection
        {
            _productservice = productser;
        }
        public IActionResult Index()
        {
            var ProductViewModel = new ProductViewModel()
            {
                Products = _productservice.GetHomePageProducts()
        };

            return View(ProductViewModel);
        }

      
    }
}
