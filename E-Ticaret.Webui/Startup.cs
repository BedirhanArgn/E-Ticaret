using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ticaret.business.Abstract;
using E_ticaret.business.Concreate;
using E_ticaret.data.Abstract;
using E_ticaret.data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
            services.AddControllersWithViews();
            //Businness katman� ui katmanda tan�tmak i�in doldur
            services.AddScoped<IProductRepository, EfCoreProductRepository>(); //�r�n katman�nda IproductRepository �apr�ld�p�nda efcoreproduct'�n �a��mas�n� sa�lamak icin yazd�l.
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>(); //�r�n katman�nda IproductRepository �apr�ld�p�nda efcoreproduct'�n �a��mas�n� sa�lamak icin yazd�l.
            services.AddScoped<IProductService, ProductManager>(); //�r�n katman�nda IproductRepository �apr�ld�p�nda efcoreproduct'�n �a��mas�n� sa�lamak icin yazd�l.
            services.AddScoped<ICategoryService, CategoryManager>(); //�r�n katman�nda IproductRepository �apr�ld�p�nda efcoreproduct'�n �a��mas�n� sa�lamak icin yazd�l.
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
        
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products",
                    defaults: new { controller = "Admin", action = "ProductList" }

                    );

               endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "Edit" }

                    );
                
                endpoints.MapControllerRoute(
                    name:"search",
                    pattern:"search",
                    defaults: new {controller="Shop",action="search"}
                    );

                endpoints.MapControllerRoute(
                    name:"about",
                    pattern:"about",
                    defaults: new {controller="Shop",action="about"}
                    );

                endpoints.MapControllerRoute(
                       name: "productdetails",
                       pattern: "{productname}",
                       defaults: new { controller = "Shop", action = "details" }
                       );

                endpoints.MapControllerRoute(
                    name:"products",
                    pattern:"products/{category?}",
                    defaults: new {controller="Shop",action="list"}  
                    );
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    
                    
                    );
            });
        }
    }
}
