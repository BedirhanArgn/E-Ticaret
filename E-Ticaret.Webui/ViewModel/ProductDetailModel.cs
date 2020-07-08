using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.ViewModel
{
    public class ProductDetailModel
    {
            public Product product { get; set; }
            public List<Category> Categories { get; set; }
    }
}
