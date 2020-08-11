using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class CardModel
    {
        public int CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }

        public double TotalPrice()
        {
            return CartItems.Sum(i => i.Price * i.Quantity);
        }

    }

    public class  CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    
    
    
    }



}
