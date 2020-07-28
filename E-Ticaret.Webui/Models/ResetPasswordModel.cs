using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class ResetPasswordModel
    {

        [Required]
        public string Token { get; set; }
       
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password alanı zorunludur  ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }

}
