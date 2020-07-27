using E_Ticaret.Webui.EmailServices;
using E_Ticaret.Webui.Identity;
using E_Ticaret.Webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Controllers
{
    [AutoValidateAntiforgeryToken] //her postta csrf ataklarının önüne geçer 
    public class AccountController : Controller
    {
        private UserManager<User> _usermanager; //kullanıcı yönetimi
        private SignInManager<User> _signInManager; //cookie yönetimi
        private IEmailService _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {

            _usermanager = userManager;
            _signInManager = signInManager;
            _emailSender = emailService;
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
        [ValidateAntiForgeryToken]
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

            if (!await _usermanager.IsEmailConfirmedAsync(user))
            {

                ModelState.AddModelError("", "Lütfen hesabınıza gelen maili onaylayınız!");
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
        [ValidateAntiForgeryToken] //csrf ataklarının önüne geçer
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
                var code = await _usermanager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmedEmail","Account",new {  //bu kısım url oluşturuyor.
                   userId=user.Id,
                   token=code 
                });
                await _emailSender.SendEmailAsync(model.Email, "Hesabınız onaylayınız",$"Lütfen email hesabınızı onaylanmak için linke <a href='https://localhost:44317{url}'>tıklayınız</a> ");
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("Password","Bilinmeyen bir hata oldu");//Burdan hata ekleyebilirsin(Başka bir hata olduysa böyle gönderebilirsin)
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync(); //cookiyi temizler.
            return Redirect("~/");
            
        }

        public async Task<IActionResult> ConfirmedEmail(string userId,string token)
        {
            if(userId==null||token==null)
            {
                CreateMessage("Geçersiz token", "danger");
                return View();
            }
            var user = await _usermanager.FindByIdAsync(userId);
            if(user!=null)
            { 
                var result = await _usermanager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    CreateMessage("Hesabınız onaylandı", "success");
                    return View();
                }
            }
            CreateMessage("Hesabınız onaylanmadı", "danger");
            return View();

        }

        public void CreateMessage(string message, string type)
        {
            var msg = new AlertMessage()
            {
                AlertType = type,
                Message = message
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);


        }

    }
}

