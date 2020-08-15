using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Identity
{
    public class ApplicationContext:IdentityDbContext<User> 
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) //Daha sonra Startup.cs de tanımlamaları yap
        {

            
        }
        
    }
}
