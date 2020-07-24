using E_Ticaret.Webui.Identity;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _usermanager; //kullanıcı yönetimi
        private SignInManager<User> _signInManager; //cookie yönetimi

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _usermanager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
      }

        [HttpPost]
        public  async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _usermanager.CreateAsync(user, model.Password); //Bu kısım parola üretip şifreliyor.

            if(result.Succeeded)
            {
                    //Eğer mail dorulama yapmak istersen bu kısımda bir token oluştur.              
                return RedirectToAction("Login", "Account");
            }

            return View();
        }


    }
}

