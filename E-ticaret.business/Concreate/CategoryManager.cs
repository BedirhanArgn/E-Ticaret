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
            _categoryRepository.Create(entity);
        }
        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            _categoryRepository.DeleteFromCategory(productId, categoryId);
        }

        public List<Category> getAll()
        {
            return _categoryRepository.getAll();
            
        }

        public Category getById(int id)
        {
            return _categoryRepository.getById(id);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return _categoryRepository.GetByIdWithProducts(categoryId);
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}
