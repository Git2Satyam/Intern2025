using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public Guid CartId { get; set; }
        public bool? IsActive { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public virtual List<ProductModel> Products { get; set; }
    }
}
