using E_Ticaret.Webui.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }

    //Roller listeleniyor ve o rollere ait üyeler ve ait olmayan üyelere erişim sağlanıyor.
    public class RoleDetails
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<User> User { get; set; }
        public IEnumerable<User> Members { get; set; }  //O role ait olan ve olmayan kullanıcılara erişmemiz gerek. 
        public IEnumerable<User> NonMembers { get; set; }

    }
    //role eklenecekler ve silinecekleri tutuyor.
    public class RoleEditModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; } //role eklenecek olanların ıd'lerinin tutulduğu stringdir.
        public string[] IdsToDelete { get; set; } //rolden çıkarılacak olanların id'lerin tutulduğu stringdir.
    }


}
