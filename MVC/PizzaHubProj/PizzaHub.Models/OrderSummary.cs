using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Models
{
    public class OrderSummary
    {
        public decimal? TaxAmount {  get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PayingAmount { get; set; }
    }
}
