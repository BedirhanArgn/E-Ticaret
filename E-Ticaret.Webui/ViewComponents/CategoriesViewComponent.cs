using System.Collections.Generic;
using E_ticaret.business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents { 
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
             if (RouteData.Values["category"] !=null)
            {
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            }
                 
             return View(_categoryService.getAll());
        }
    }
}