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
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);

    }
}
