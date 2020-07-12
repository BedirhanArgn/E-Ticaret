using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.Entity;
namespace E_Ticaret.ViewModel
{
   public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

      public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
