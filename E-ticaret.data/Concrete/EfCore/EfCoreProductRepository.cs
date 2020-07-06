using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{

    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public List<Product> GetPopularProduct()
        {
            using (var context = new ShopContext())
            {

                return context.Products.ToList();

            }

        }
        public List<Product> GetTop5()
        {
           using(var context=new ShopContext())
            {
                return context.Products.OrderByDescending(i=>i.ProductId).Take(5).ToList();

            }
        }
    }
}
