using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.EmailServices
{
    public class SmtpEmailSender : IEmailService
    {

        private int _port;
        private string _host;
        private bool _enableSSL;
        private string _username;
        private string _password;
        public SmtpEmailSender(string host,int port,bool enableSSl,string username,string password)
        {
            this._host = host;
            this._port = port;
            this._enableSSL = enableSSl;
            this._username = username;
            this._password = password;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient (this._host,this._port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = _enableSSL,
              
                
                
            };
            return client.SendMailAsync(
                new MailMessage(this._username,email,subject,htmlMessage)
                {
                    IsBodyHtml=true
                }
                
                ); 


        }
    }
}
