﻿using E_Ticaret.Webui.Identity;
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
        public IActionResult Login(string ReturnUrl=null) //kullanıcıyı loginden önceyi sayfaya yönlendirmemiz gerek login olduktan sonra bu yüzden return urli alıyoruz burada
        {
            return View(new LoginModel
            {
                ReturnUrl = ReturnUrl //bunu daha sonra input hiddenda tutarız (yanlis bir şeyler girildiğinde tekrar post metodundan geleceği için return url boş gelmesin)
            }) ;
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usermanager.FindByEmailAsync(model.Email); //username var mı diye control

            if(user==null)
            {
                ModelState.AddModelError("", "Bu email adresi ile daha önce hespa oluşturulmamış");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true,false); //4.parametre 20 dk lık seesion süresi tanımlansın mı ,5.parametre hesap kilitlesin mi

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/"); //null sa home pageye git dedik.
            }
            ModelState.AddModelError("", "Girilen email veya şifre yanlış");

            return View(model);
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


        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return Redirect("~/");
            
        }



    }
}

