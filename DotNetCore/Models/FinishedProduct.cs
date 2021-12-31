using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Models
{
    public class FinishedProduct
    {
        public int FinishedProductId { get; set; }
        [Display(Name = "Product Name")]
        public string FinishedProductName { get; set; }
        [Display(Name = "Product Description")]
        public string FinishedProductDescription { get; set; }
        public virtual ICollection<ComponentProduct> ComponentProducts { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
