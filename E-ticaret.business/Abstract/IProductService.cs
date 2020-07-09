using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
   public interface IProductService
    {
       Product getById(int id);
        Product getProductDetails(int id);
        List<Product> GetProductsByCategory(string name);
        List<Product> getAll();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

    }
}
