using System;
using System.Collections.Generic;
using System.Text;

namespace E_ticaret.Entity
{
    public class CardItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public int Quantity { get; set; }
    }
}
