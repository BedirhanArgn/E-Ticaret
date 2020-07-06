using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productrepository;
        public HomeController(IProductRepository productRepository) //Depedency Injection
        {
            _productrepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
