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

        public void AddToCart(string userId, int productId, int quantity)
        {
            var card = GetCardByUserId(userId);

            if (card != null)
            {
                //eklenmek isteyen ürün sepette var mı?
                var index = card.CardItems.FindIndex(i => i.ProductId == productId); //bu metod arar ve indexini döner geriye
                if (index < 0)
                {
                    card.CardItems.Add(new CardItem()
                    {
                        CardId = card.Id,
                        ProductId=productId,
                        Quantity=quantity
                    });
    
                }
                else
                {
                    card.CardItems[index].Quantity += quantity;
                }
                _cardRepository.Update(card);


            }

        }

        public void DeleteFromCart(string userId, int productId)
        {
            var card = GetCardByUserId(userId);
            if(card!=null)
            {
                _cardRepository.DeleteFromCard(card.Id,productId);
            }

        }

        public Card GetCardByUserId(string userId)
        {
            return _cardRepository.GetByUserId(userId);
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
