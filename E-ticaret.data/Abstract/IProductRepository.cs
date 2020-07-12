using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(string productname);
        List<Product> GetPopularProduct();
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        List<Product> GetHomePageProducts();
        int getCountByCategory(string category);


    }
}
