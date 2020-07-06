using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public double? Price { get; set; }
        public string Description { get; set; }

        public String ImageUrl { get; set; }
        public bool IsApproved { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

    }
}
