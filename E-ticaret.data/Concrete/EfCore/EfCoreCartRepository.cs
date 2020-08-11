using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Card, ShopContext>, ICardRepository
    {
        public void DeleteFromCard(int cartId, int productId)
        {
            using(var context=new ShopContext())
            {
                var item=context.CardItems.Where(i => i.CardId == cartId && i.ProductId == productId).FirstOrDefault();
                context.CardItems.Remove(item);
                context.SaveChanges();


              //  var cmd = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
               // context.Database.ExecuteSqlRaw(cmd, cartId, productId);

            }



        }

        public Card GetByUserId(string userId)
        { 
            using(var context=new ShopContext())
            {
                return context.Cards.Include(i => i.CardItems).ThenInclude(i => i.Product).FirstOrDefault(i=>i.UserId==userId);

            }

        }

        public override void Update(Card entity)
        {
            using(var context=new ShopContext())
            {
                context.Cards.Update(entity);
                context.SaveChanges();

            }


        }


    }
}
