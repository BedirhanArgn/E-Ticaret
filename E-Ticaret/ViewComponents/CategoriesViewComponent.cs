using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents { 
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // if (RouteData.Values["action"].ToString()=="list")
            //     ViewBag.SelectedCategory = RouteData?.Values["id"];
            // return View(CategoryRepository.Categories);
            return View();
        }
    }
}