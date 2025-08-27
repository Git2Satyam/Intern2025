using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Models
{
    public class CheckoutModel
    {
        public List<ProductModel> Products { get; set; }
        public OrderSummaryModel OrderSummary { get; set; }
    }
}
