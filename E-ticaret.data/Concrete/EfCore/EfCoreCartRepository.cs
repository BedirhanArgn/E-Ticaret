using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{
    public class EfCoreCartRepository:EfCoreGenericRepository<Card,ShopContext>,ICardRepository
    {
    }
}
