using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.Entity
{
    //Junction Table fluent api ile bildir daha sonra olışturulması için 
   public  class ProductCategory
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
