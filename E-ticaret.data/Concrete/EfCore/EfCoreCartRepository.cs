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
        public Card GetByUserId(string userId)
        { 
            using(var context=new ShopContext())
            {
                return context.Cards.Include(i => i.CardItems).ThenInclude(i => i.Product).FirstOrDefault(i=>i.UserId==userId);

            }

        }
    }
}
