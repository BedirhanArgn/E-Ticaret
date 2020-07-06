using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetPopularProduct();

        List<Product> GetTop5();

    }
}
