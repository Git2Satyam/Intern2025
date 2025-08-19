using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Models
{
    public class CartItemModel
    {
        public int id { get; set; }
        public Guid CartId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Currency { get; set; }
        public ProductModel Products { get; set; }

    }
}
