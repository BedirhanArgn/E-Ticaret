using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Name alanı zorunludur  ")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Kategori için 5-100 arasında bir değer alır.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Url Alanı ZORUNLUDUR.")]
        public string Url { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } //Category yanında o categorinin productlarını da getir.

    }
}
