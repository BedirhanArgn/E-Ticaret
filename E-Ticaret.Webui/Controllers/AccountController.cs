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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usermanager.FindByNameAsync(model.UserName); //Var mı diye control

            if(user==null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce hespa oluşturulmamış");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false,false); //5.parametre hesap kilitlesin mi

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

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

            ModelState.AddModelError("Password","Bilinmeyen bir hata oldu");//Burdan hata ekleyebilirsin(Başka bir hata olduysa böyle gönderebilirsin)
            return View();
        }


    }
}

