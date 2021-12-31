using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Models
{
    public class UnitOfMeasure
    {
        public int UnitOfMeasureId { get; set; }
        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasureDescription { get; set; }
    }
}
