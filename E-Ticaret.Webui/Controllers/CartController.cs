using E_ticaret.business.Abstract;
using E_Ticaret.Webui.Identity;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Ticaret.Webui.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICardService _cardService;
        private UserManager<User> _userManager;

        public CartController(ICardService cardService, UserManager<User> userManager)
        {
            _cardService = cardService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cardService.GetCardByUserId(_userManager.GetUserId(User));
            return View(new CardModel()
            {
                CartId = cart.Id,
                CartItems = cart.CardItems.Select(i => new CartItemModel()
                {
                    CartItemId=i.Id,
                    Name=i.Product.Name,
                    Price=(double)i.Product.Price,
                    ImageUrl=i.Product.ImageUrl,
                    ProductId=i.ProductId,
                    Quantity=i.Quantity
                    
                }).ToList()

            }) ;
        }

        [HttpPost]
        public IActionResult AddToCard()
        {
            return View();
        }
    }
}
