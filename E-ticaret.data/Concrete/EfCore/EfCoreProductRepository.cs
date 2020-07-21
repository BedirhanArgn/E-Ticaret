using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{

    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.ProductId == id).Include(a => a.ProductCategories).ThenInclude(b => b.Category).FirstOrDefault();
            }

        }
        public int getCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                   .Include(i => i.ProductCategories)
                                   .ThenInclude(i => i.Category)
                                   .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));
                }
                return products.Count();
                //skip atlar ilk beş ürünü atladı ondan sonra atlanılan yerdeki beş ürünü aldı
            }
        }
        public List<Product> GetHomePageProducts()
        {
            using (var context = new ShopContext())
            {

                return context.Products.Where(i => i.IsApproved && i.IsHome).ToList();

            }
        }
        public List<Product> GetPopularProduct()
        {
            using (var context = new ShopContext())
            {

                return context.Products.ToList();

            }

        }

        public Product GetProductDetails(string url)
        {
            using (var context = new ShopContext())
            {
                return context.Products.Where(i => i.Url == url).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
                //manyto many tablodan productid'ye karşılık gelen categoryid den categorye geçiş yaptık.
                //ilgili productın categorysi varsa onları getirir.            
            }


        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                   .Include(i => i.ProductCategories)
                                   .ThenInclude(i => i.Category)
                                   .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
                }


                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                //skip atlar ilk beş ürünü atladı ondan sonra atlanılan yerdeki beş ürünü aldı
            }

        }

        public List<Product> GetSearchResult(string searchString)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || (i.Description.ToLower().Contains(searchString.ToLower()))));

                return products.ToList();
            }
        }
        public List<Product> GetTop5()
        {
            using (var context = new ShopContext())
            {
                return context.Products.OrderByDescending(i => i.ProductId).Take(5).ToList();

            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {

                var product = context.Products.Include(i => i.ProductCategories).FirstOrDefault(i => i.ProductId == entity.ProductId);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.Url = entity.Url;
                    product.ImageUrl = entity.ImageUrl;
                    product.IsHome = entity.IsHome;
                    product.IsApproved = entity.IsApproved;
                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        ProductId = entity.ProductId,
                        CategoryId=catid

                    }).ToList() ;
                    context.SaveChanges();
                }
            }


        }
    }
}
