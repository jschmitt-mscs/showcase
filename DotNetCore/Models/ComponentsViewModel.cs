using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Models
{
    public class ComponentsViewModel
    {
        [Key]
        [Display(Name ="Product Name")]
        public string productName { get; set; }
        [Display(Name = "Product Description")]
        public string productDescription { get; set; }
        [Display(Name = "Final Products Included In")]
        public int finalProductCount { get; set; }
    }
}
