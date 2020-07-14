using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Webui.Models
{
    public class ProductView
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public double? Price { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public String ImageUrl { get; set; }
        public bool IsHome { get; set; } //Eğer home page de göster dersek göstereceğiz.
        public bool IsApproved { get; set; }


    }
}
