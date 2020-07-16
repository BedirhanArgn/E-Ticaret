﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Newtonsoft.Json;

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
            //ViewData["message"] = $"{entity.Name} isimli ürün eklendi"; //farklı bir actiona redirect olduğu için çalışmaz.


            var msg = new AlertMessage()
            {
                AlertType = "success",
                Message = $"{ entity.Name } isimli ürün eklendi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

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

            var msg = new AlertMessage()
            {
                AlertType = "success",
                Message = $"{ entity.Name } isimli ürün güncellendi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.getById(productId);

            if(entity!=null)
            {
                _productService.Delete(entity);
            }
            var msg = new AlertMessage()
            {
                AlertType = "danger",
                Message = $"{ entity.Name } isimli ürün silindi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }


    }
}
