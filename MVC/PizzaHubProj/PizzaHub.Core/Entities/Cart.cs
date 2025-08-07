using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Core.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
