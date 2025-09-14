using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Models
{
    public class CheckoutModel
    {
        public List<ProductModel>? Products { get; set; }
        public OrderSummary? Order { get; set; }
       
    }
}
