using E_ticaret.business.Abstract;
using E_ticaret.Entity;
using E_Ticaret.ViewModel;
using E_Ticaret.Webui.ViewModel;
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

        public IActionResult List(string category,int page=1) //sayfa sayısı gelmezse 1 i göster
        {
            const int pageSize = 3;
            var ProductViewModel = new ProductViewModel()
            {

                PageInfo = new PageInfo()
                {
                    TotalItems = _productservice.getCountByCategory(category),
                    CurrentPage=page,
                    ItemsPerPage=pageSize,
                    CurrentCategory=category
                },
                Products = _productservice.GetProductsByCategory(category,page,pageSize)
            };

            return View(ProductViewModel);

        }
        public IActionResult Details(string productname)
        {
            if(productname == null)
            {
                return NotFound();
            }
            Product product = _productservice.getProductDetails(productname);
            if(product==null)
            {
                return NotFound();
            }
            return View(new ProductDetailModel
            {
                product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
  
            }) ;
            
        }
        public IActionResult Search(string q)
        {

            var product = new ProductViewModel()
            {
                Products = _productservice.getSearchResult(q)

        };

            
            return View(product);

        }



    }
}
