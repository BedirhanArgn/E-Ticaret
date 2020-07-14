using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CreateProduct(ProductView product)
        {
            var entity = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Url = product.Url,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

            _productService.Create(entity);
            return RedirectToAction("ProductList");
        }

    }
}
