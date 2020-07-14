using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_Ticaret.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Webui.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult ProductList()
        {
            return View(new ProductViewModel()
            {

                Products = _productService.getAll()

            }) ;
        }
    }
}
