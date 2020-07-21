using E_ticaret.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class ProductView
    {
        public int ProductId { get; set; }
      //  [Required(ErrorMessage ="Bu alan zorunludur")]
        //[Display(Name ="Name",Prompt ="Enter a Name")]
        public string Name { get; set; }
        
        //[Required(ErrorMessage = "Price Bu alan zorunludur")]
        public double? Price { get; set; }

       // [Required(ErrorMessage = "Description bu alan zorunludur")]
        public string Description { get; set; }
        //[Required(ErrorMessage="Url bu alan zorunludur.")]
        public string Url { get; set; }
        public String ImageUrl { get; set; }
        public bool IsHome { get; set; } //Eğer home page de göster dersek göstereceğiz.
        public bool IsApproved { get; set; }

        public List<Category> SelectedCategories { get; set; } //Product eklerken hangi kategorilere ait olmasını seçmek icin yanında categorileri de taşı

    }
}
