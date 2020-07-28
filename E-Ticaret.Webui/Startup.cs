using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.business.Concreate;
using E_ticaret.data.Abstract;
using E_ticaret.data.Concrete.EfCore;
using E_Ticaret.Webui.EmailServices;
using E_Ticaret.Webui.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace E_Ticaret.Webui
{
    public class Startup
    {

        private IConfiguration  _configuration;
    
        public Startup(IConfiguration configuration)  //bunun üzerinden appconfig.json daki verilere ulaþabiliyotsun
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IDENTITY DEFINATION
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Server=DESKTOP-CH0UVQ2; Database=shopDb; User Id=sa; Password=bedir123456;"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders(); //kullanýcýlarý authotechticate için yazdýk tarayýcýya cokie býraktýk.Her seferinde logine atmasýn diye 
            /**************************************************************************************************/
            services.AddSingleton<IConfiguration>(_configuration);
         
            services.AddScoped<IEmailService, SmtpEmailSender>(i => new SmtpEmailSender(_configuration["EmailSender:Host"], _configuration.GetValue<int>("EmailSender:Port"),
               _configuration.GetValue<bool>("EmailSender:EnableSSL"), _configuration["EmailSender:UserName"], _configuration["EmailSender:Password"])); //Burada ayarlarý appsettingten almak için constructor yaratmalýsýn.
      
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false; //Sayý olmalý
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false; //@ * gibi karakterler olmalý

                options.Lockout.MaxFailedAccessAttempts = 5; //5 giriþten sonra kilitlenioyr. 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //5 dk sonra açýlýr
                options.Lockout.AllowedForNewUsers = true; //üsttekilerle alakalý

                //options.User.AllowedUserNameCharacters = ""; //olmasýný istediðiniz kesin karaterrleri yaz

                options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun her kullanýcýnýn 

                options.SignIn.RequireConfirmedEmail = true; //Kayýt olduktan email ile token gönderecek 
                options.SignIn.RequireConfirmedPhoneNumber = false; //telefon doðrulamasý

            });

            services.ConfigureApplicationCookie(option => //cookie burada yaratýlýr.
            {
                option.LoginPath = "/account/login";
                option.LogoutPath = "/account/logout";
                option.AccessDeniedPath = "/account/accessdenied"; //yanlýþ yere girenler için gereklidir. 
                option.SlidingExpiration = true; //session süresi 20 dk dýr 20 dk boyunca herhangi bir istek gelmezse oturum kapatýlýr. 
                option.ExpireTimeSpan = TimeSpan.FromMinutes(36); //36 dk'lýk bir session oluþtur.

                option.Cookie = new CookieBuilder
                {
                    HttpOnly = true, //cookie'yi sadece http olarak alabiliriz.
                    Name = ".Shopapp.Security.Cookie",
                    SameSite = SameSiteMode.Strict //B kullanýcýsý Anýn cookiesine sahip olsa bile onun adýna iþlem ypaamz bunu yazarsak 
                };


            });



            services.AddControllersWithViews();
            //Businness katmaný ui katmanda tanýtmak için doldur
            services.AddScoped<IProductRepository, EfCoreProductRepository>(); //Ürün katmanýnda IproductRepository çaprýldýpýnda efcoreproduct'ýn çaýþmasýný saðlamak icin yazdýl.
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>(); //Ürün katmanýnda IproductRepository çaprýldýpýnda efcoreproduct'ýn çaýþmasýný saðlamak icin yazdýl.
            services.AddScoped<IProductService, ProductManager>(); //Ürün katmanýnda IproductRepository çaprýldýpýnda efcoreproduct'ýn çaýþmasýný saðlamak icin yazdýl.
            services.AddScoped<ICategoryService, CategoryManager>(); //Ürün katmanýnda IproductRepository çaprýldýpýnda efcoreproduct'ýn çaýþmasýný saðlamak icin yazdýl.
            services.AddControllersWithViews();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication(); //Identity için auth'ý açtýk.
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllerRoute(
                   name: "admincategory",
                   pattern: "admin/categories",
                   defaults: new { controller = "Admin", action = "CategoryList" }
                   );

               endpoints.MapControllerRoute(
                   name: "admincategorycreate",
                   pattern: "admin/categories/create",
                   defaults: new { controller = "Admin", action = "CategoryCreate" }
                   );

               endpoints.MapControllerRoute(
                  name: "admincategorydelete",
                  pattern: "admin/deletecategory",
                  defaults: new { controller = "Admin", action = "CategoryDelete" }
                  );

               endpoints.MapControllerRoute(
                   name:"admincategoryedit",
                   pattern:"admin/categories/{id?}",
                   defaults: new {controller="Admin",action="CategoryEdit"}             
                   );

               endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }

                    );
               endpoints.MapControllerRoute(
                  name: "admindeleteproduct",
                  pattern: "admin/deleteproduct",
                  defaults: new { controller = "Admin", action = "DeleteProduct" }

                  );

               endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "Edit" }

                    );

               endpoints.MapControllerRoute(
                   name: "search",
                   pattern: "search",
                   defaults: new { controller = "Shop", action = "search" }
                   );

               endpoints.MapControllerRoute(
                   name: "about",
                   pattern: "about",
                   defaults: new { controller = "Shop", action = "about" }
                   );

               endpoints.MapControllerRoute(
                      name: "productdetails",
                      pattern: "{productname}",
                      defaults: new { controller = "Shop", action = "details" }
                      );

               endpoints.MapControllerRoute(
                   name: "products",
                   pattern: "products/{category?}",
                   defaults: new { controller = "Shop", action = "list" }
                   );

               endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}"


                   );
           });
        }
    }
}
