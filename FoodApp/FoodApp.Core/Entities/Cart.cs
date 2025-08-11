using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }

    }
}
