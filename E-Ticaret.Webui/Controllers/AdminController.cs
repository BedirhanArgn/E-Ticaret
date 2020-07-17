using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.Models;
using E_Ticaret.Webui.ViewModel;
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

        private ICategoryService _categoryService;
        public AdminController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
            var product = _productService.GetByIdWithCategories((int)id);
            if(product==null)
            {
                return NotFound();
            }
            var model = new ProductView()
            {
                ProductId = product.ProductId,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Url = product.Url,
                Name = product.Name,
                Price = product.Price,
                SelectedCategories = product.ProductCategories.Select(i => i.Category).ToList() //seçili kategoriler edit için
            }; 
            ViewBag.Categories = _categoryService.getAll(); //hepsi 


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

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.getAll()
            }) ;
        }
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel category)
        {
           
            var entity = new Category()
            {
                Description = category.Description,
                CategoryId = category.CategoryId,
                Name = category.Name,
                Url = category.Url
            };
            _categoryService.Create(entity);
          
            var msg = new AlertMessage()
            {
                AlertType = "success",
                Message = $"{ entity.Name } isimli kategori eklendi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var ExistControl = _categoryService.GetByIdWithProducts((int)id);
            if(ExistControl==null)
            {
                return NotFound();
            }


            var model = new CategoryModel()
            {
                CategoryId = ExistControl.CategoryId,
                Description = ExistControl.Description,
                Name = ExistControl.Name,
                Url = ExistControl.Url,
                Products = ExistControl.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }

        [HttpPost]

        public IActionResult CategoryEdit(CategoryModel cat)
        {

            var entity = _categoryService.GetByIdWithProducts(cat.CategoryId);

            if(entity!=null)
            {
                entity.CategoryId = cat.CategoryId;
                entity.Description = cat.Description;
                entity.Name = cat.Name;
                entity.Url = cat.Url;
            }

            _categoryService.Update(entity);
            var msg = new AlertMessage()
            {
                AlertType = "success",
                Message = $"{ entity.Name } isimli kategori güncellendi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList");
        }

        public IActionResult CategoryDelete(int? categoryid)
        {
            var entity = _categoryService.getById((int)categoryid);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            var msg = new AlertMessage()
            {
                AlertType = "danger",
                Message = $"{ entity.Name } isimli kategori silindi"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }


        [HttpPost]
        public IActionResult DeleteFromCategory(int productid,int categoryId)
        {
            _categoryService.DeleteFromCategory(productid, categoryId);
            return RedirectToAction("CategoryList");
        }


    }
}
