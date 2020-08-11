using E_ticaret.data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Generic bir doldurma sınıfı fonksiyonları her sınıf için ayrı ayrı implemente etmeye gerek kalmadı.
namespace E_ticaret.data.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext,new()
    {
        public void Create(TEntity entity)
        {
            using (var context=new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }

        }
       public void Delete(TEntity entity )
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public List<TEntity> getAll()
        {
            using (var context = new TContext())
            {
                 return context.Set<TEntity>().ToList();
            }
        }

        public TEntity getById(int id)

        {
            using (var context=new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }

        }

        public virtual void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified; //Değiştirilen alanı update eder sadece
                context.SaveChanges();
            }
        }
    }
}
