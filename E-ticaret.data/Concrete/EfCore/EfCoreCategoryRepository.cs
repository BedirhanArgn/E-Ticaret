using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {

                var entity = context.ProductCategories.Where(i => i.CategoryId == categoryId &&i.ProductId==productId).FirstOrDefault();
                context.ProductCategories.Remove(entity);
                context.SaveChanges();

            }
        }

        public Category GetByIdWithProducts(int categoryid)
        {
            using(var context=new ShopContext())
            {

                return context.Categories.Where(i => i.CategoryId == categoryid)
                    .Include(a => a.ProductCategories)
                    .ThenInclude(b => b.Product).FirstOrDefault();

            }
        }

        public List<Category> getPopularCategory()
        {
            throw new NotImplementedException();
        }
    }
}
