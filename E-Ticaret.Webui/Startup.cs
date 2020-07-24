using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.business.Concreate;
using E_ticaret.data.Abstract;
using E_ticaret.data.Concrete.EfCore;
using E_Ticaret.Webui.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E_Ticaret.Webui
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IDENTITY DEFINATION
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Server=DESKTOP-CH0UVQ2; Database=shopDb; User Id=sa; Password=bedir123456;"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders(); //kullanýcýlarý authotechticate için yazdýk tarayýcýya cokie býraktýk.Her seferinde logine atmasýn diye 
            /**************************************************************************************************/

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
