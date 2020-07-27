using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.EmailServices
{
    public interface IEmailService
    {

        Task SendEmailAsync(string email, string subject, string htmlMessage);  
    }
}
