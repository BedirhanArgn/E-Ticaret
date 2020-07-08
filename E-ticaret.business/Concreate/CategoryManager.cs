using E_ticaret.business.Abstract;
using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace E_ticaret.business.Concreate
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public List<Category> getAll()
        {
            return _categoryRepository.getAll();
            
        }

        public Category getById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
