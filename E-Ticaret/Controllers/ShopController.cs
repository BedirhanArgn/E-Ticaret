using E_ticaret.business.Abstract;
using E_Ticaret.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productservice;
        public ShopController(IProductService productService)
        {
            _productservice = productService;
        }

        public IActionResult List()
        {
            var ProductViewModel = new ProductViewModel()
            {
                Products = _productservice.getAll()
            };

            return View(ProductViewModel);

        }



    }
}
