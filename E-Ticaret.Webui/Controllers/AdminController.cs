using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _productService.getById((int)id);
            if(product==null)
            {
                return NotFound();
            }
            var model = new ProductView()
            {
                ProductId=product.ProductId,
                Description=product.Description,
                ImageUrl=product.ImageUrl,
                Url=product.Url,
                Name=product.Name,
                Price=product.Price
            };

            return View(model);  

        }

        [HttpPost]
        public IActionResult Edit(ProductView model)
        {
            var entity = _productService.getById(model.ProductId);
            if(entity==null)
            {
                return NotFound();
            }

            entity.ProductId = model.ProductId;
            entity.ImageUrl = model.ImageUrl;
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Url = model.Url;
            entity.Description = model.Description;
            _productService.Update(entity);
            return RedirectToAction("ProductList");
        }

    }
}
