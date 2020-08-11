using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Abstract
{
    public interface ICardService
    {
        void InitializeCart(string userId);

        Card GetCardByUserId(string userId);

        void AddToCart(string userId, int productId, int quantity);
    }
}
