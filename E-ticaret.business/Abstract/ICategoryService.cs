using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
    public interface ICategoryService
    {

        Category getById(int id);
        List<Category> getAll();
        Category GetByIdWithProducts(int categoryId);
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        void DeleteFromCategory(int productId, int categoryId);
    }
}
