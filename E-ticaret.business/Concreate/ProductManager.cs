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
        public void Create(Product entity)
        {
            //İş kuralları uygula bazı kurallar uygulayarak ekle 
            _productRepository.Create(entity);
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
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
