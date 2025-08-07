using PizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Services.Interface
{
    public interface ICartService
    {
        public bool AddItemToCart(Guid cartId, int productId);

        IEnumerable<CartItemModel> GetCartItems();

    }
}
