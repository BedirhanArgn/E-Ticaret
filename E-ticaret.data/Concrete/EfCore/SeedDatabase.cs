using E_ticaret.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace E_ticaret.data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count() == 0)  //Bekleyen migrations var mı kontrol liste döndürür. Eğer yoksa 
            {
                if (context.Categories.Count() == 0) //Eğer db'deki category sayısı sıfırsa 
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Products.Count() == 0) //Eğer db'deki category sayısı sıfırsa 
                {
                    context.Products.AddRange(Products);
                    context.AddRange(productCategories);
                }
                context.SaveChanges();
            }


        }
        public static Category[] Categories =
        {
            new Category(){Name="Telefon",Url="telefon"},
            new Category() {Name="Bilgisayar",Url="bilgisayar"},
            new Category(){Name="Elektronik",Url="elektronik"},
            new Category(){Name="Beyaz Eşya",Url="beyaz-esya"}


        };

        public static Product[] Products =
        {
            new Product() {Name="Sony Xperia Xa",Description="Sonynin güzel telefonu",Price=2000,ImageUrl="1.jpg",IsApproved=true},
            new Product() {Name="Sony Xperia Z1",Description="Sonynin güzel telefonu",Price=1000,ImageUrl="2.jpg",IsApproved=true},
            new Product() {Name="Sony Xperia Z2",Description="Sonynin güzel telefonu",Price=3000,ImageUrl="3.jpg",IsApproved=false},
            new Product() {Name="Sony Xperia Z3",Description="Sonynin güzel telefonu",Price=4000,ImageUrl="4.jpg",IsApproved=false},
            new Product() {Name="Sony Xperia Xa Ultra",Description="Sonynin güzel telefonu",Price=5000,ImageUrl="5.jpg",IsApproved=false},
            new Product() {Name="Sony Xperia Z4",Description="Sonynin güzel telefonu",Price=7000,ImageUrl="6.jpg",IsApproved=true}

        };

        private static ProductCategory[] productCategories =
        {
            new ProductCategory(){Product=Products[0],Category=Categories[0] },
            new ProductCategory(){Product=Products[0],Category=Categories[2] },
            new ProductCategory(){Product=Products[1],Category=Categories[0]},
            new ProductCategory(){Product=Products[1],Category=Categories[2] },

            new ProductCategory(){Product=Products[2],Category=Categories[0] },
            new ProductCategory(){Product=Products[2],Category=Categories[2] },
            new ProductCategory(){Product=Products[3],Category=Categories[0]},
            new ProductCategory(){Product=Products[3],Category=Categories[2] },

            new ProductCategory(){Product=Products[4],Category=Categories[0] },
            new ProductCategory(){Product=Products[4],Category=Categories[2]},
              new ProductCategory(){Product=Products[5],Category=Categories[0]},
            new ProductCategory(){Product=Products[5],Category=Categories[2] },
        };

    }
}

//Bunları ekledikten sonra web.ui kısmında  startup.cs'in configure kısmında env.IsDevelopment kısmında 
//SeedDatabase.Seed(); metodunu çağıralım.
