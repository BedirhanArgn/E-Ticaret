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

        public IActionResult List(string category)
        {
            var ProductViewModel = new ProductViewModel()
            {
                Products = _productservice.GetProductsByCategory(category)
            };

            return View(ProductViewModel);

        }
        public IActionResult Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Product product = _productservice.getProductDetails((int)id);
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



    }
}
