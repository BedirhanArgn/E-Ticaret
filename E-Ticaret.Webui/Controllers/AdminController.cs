using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.Extension;
using E_Ticaret.Webui.Identity;
using E_Ticaret.Webui.Models;
using E_Ticaret.Webui.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Newtonsoft.Json;

namespace E_Ticaret.Webui.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productService;

        private ICategoryService _categoryService;
        private RoleManager<IdentityRole> _roleManager; //kendi hazır ıdentityrole sınıfını kullandık kendimiz genişletmek istersek sınıfı ıdentityrole'den türeterek ek özellikler ekleyebiliriz.
        private UserManager<User> _userManager;
        public AdminController(IProductService productService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _productService = productService;
            _roleManager = roleManager;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
        
          var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();



            foreach (var item in _userManager.Users.ToList()) //tüm kullanıcılarda dön.
            {
                if (await _userManager.IsInRoleAsync(item, role.Name))
                {
                    members.Add(item);
                }
                else  //döndüğümüz kullanıcı role.Namedeki role kayıtlı mı değil mi kontrolü true false döndürüyor.
                {
                    nonmembers.Add(item);
                }
            }
            var model = new RoleDetails()
            {
                Members = members,
                NonMembers = nonmembers,
                Role = role
            };

            return View(model);
        }

    

    [HttpPost]
    public async Task<IActionResult> RoleEdit(RoleEditModel editmodel)
    {
        if (ModelState.IsValid)
        {
            //eğer boşsa boş string tanımla boş stringte foreachönemz
            foreach (var userId in editmodel.IdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (userId != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, editmodel.RoleName);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }

            } //toplu gelen user ıdleri role eklediğimiz kısım

            foreach (var userId in editmodel.IdsToDelete ?? new string[] { }) //sildiğimiz kısım
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (userId != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, editmodel.RoleName);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }

            }

        }
        return Redirect("/admin/role/" + editmodel.RoleId);
    }




        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreateAsync(string Name)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(Name));
                if(result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description); //html kısmında All dediğimiz kısımda çıkıyor bu tip hata mesajları
                    }
                }
            }
            return View();
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
            if(ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Url = product.Url,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                };

                if (_productService.Create(entity))
                {
                    //ViewData["message"] = $"{entity.Name} isimli ürün eklendi"; //farklı bir actiona redirect olduğu için çalışmaz.

                    //var msg = new AlertMessage()
                    //{
                    //    AlertType = "success",
                    //    Message = $"{ entity.Name } isimli ürün eklendi"
                    //};

                    //TempData["message"] = JsonConvert.SerializeObject(msg);

                    CreateMessage("kayıt eklendi", "success");

                    return RedirectToAction("ProductList");
                }
                CreateMessage(_productService.ErrorMessage, "success");
                return View(product);
            }
            return View(product);

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
                IsApproved=product.IsApproved,
                IsHome=product.IsHome,
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
        public async Task<IActionResult> Edit(ProductView model,int[] categoryId,IFormFile file) //resim upload için gerekli
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.getById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.ProductId = model.ProductId;
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Url = model.Url;
                entity.Description = model.Description;
                entity.IsHome = model.IsHome;
                entity.IsApproved = model.IsApproved;
                if(file!=null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    
                    using(var stream=new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                _productService.Update(entity, categoryId);

                var msg = new AlertMessage()
                {
                    AlertType = "success",
                    Message = $"{ entity.Name } isimli ürün güncellendi"
                };
                TempData["message"] = JsonConvert.SerializeObject(msg);
                return RedirectToAction("ProductList");
            }
            ViewBag.Categories = _categoryService.getAll(); //validation çalışması için ekledim Ayrıca selectedCategories bilgileri yok hata dan sonra 
            return View(model);
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
            if (ModelState.IsValid)
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
            return View(category);
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
            if (ModelState.IsValid)
            {
                if (entity != null)
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
            cat.Products = entity.ProductCategories.Select(p => p.Product).ToList();
            return View(cat);
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

        public void CreateMessage(string message,string type)
        {
            var msg = new AlertMessage()
            {
                AlertType = type,
                Message = message
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);


        }

    }
}
