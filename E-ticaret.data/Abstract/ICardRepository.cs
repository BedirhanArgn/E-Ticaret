using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.data.Abstract
{
    public interface ICardRepository:IRepository<Card>
    {

        Card GetByUserId(string userId);
    }

}
