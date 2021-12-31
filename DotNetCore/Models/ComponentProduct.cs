using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Models
{
    public class ComponentProduct
    {
        public int ComponentProductId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int FinishedProductId { get; set; }
        public virtual FinishedProduct FinishedProduct { get; set; }
        [Display(Name = "Quantity")]
        public double ComponentQuantity { get; set; }
        public int UnitOfMeasureId { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }


    }
}
