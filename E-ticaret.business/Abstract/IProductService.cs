using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
   public interface IProductService
    {
       Product getById(int id);
        Product getProductDetails(string url);
        List<Product> GetProductsByCategory(string name,int page,int pageSize);
        List<Product> GetHomePageProducts();
        List<Product> getAll();
        List<Product> getSearchResult(string searchString);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int getCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}
