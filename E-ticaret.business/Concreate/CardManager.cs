using E_ticaret.business.Abstract;
using E_ticaret.data.Abstract;
using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.business.Concreate
{
    public class CardManager : ICardService
    {
        private ICardRepository _cardRepository;
        public CardManager(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public void InitializeCart(string userId) //kullanıcı hesabını onayladığı anda bir cart initialize edilsin.
        {
            _cardRepository.Create(new Card()
            {
                UserId = userId
            });
        }
    }
}
