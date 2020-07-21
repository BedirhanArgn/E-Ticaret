using E_ticaret.business.Abstract;
using E_ticaret.data.Abstract;
using E_ticaret.data.Concrete.EfCore;
using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Concreate
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public bool Create(Product entity)
        {
            //İş kuralları uygula bazı kurallar uygulayarak ekle 

            if(Validation(entity))
            {
                _productRepository.Create(entity);
                return true;
            }
            return false;

        }

        public void Delete(Product entity)
        {
            

            _productRepository.Delete(entity);

        }

        public List<Product> getAll()
        {
            return _productRepository.getAll();

        }

        public Product getById(int id)
        {
            return _productRepository.getById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

        public int getCountByCategory(string category)
        {
            return _productRepository.getCountByCategory(category);

        }

        public List<Product> GetHomePageProducts()
        {
            return _productRepository.GetHomePageProducts();

        }

        public Product getProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }

        public List<Product> GetProductsByCategory(string name,int page,int pageSize)
        {
            return _productRepository.GetProductsByCategory(name,page,pageSize);

        }

        public List<Product> getSearchResult(string searchString)
        {
            return _productRepository.GetSearchResult(searchString);

        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productRepository.Update(entity, categoryIds);
        }
         public string ErrorMessage { get ; set ; }

        public bool Validation(Product entity)
        {
            var isvalid = true;

            if(string.IsNullOrEmpty(entity.Name)) //business katmanında yapılabilir validationlar
            {
                ErrorMessage += "ürün ismi boş geçilmez\n";
                isvalid = false;
           }

            return isvalid;


        }
    }
}
