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
        public int AddItemToCart(Guid cartId, int productId);

        IEnumerable<CartItemModel> GetCartItems();
        public int UpdateQuantity(string cartId, int productId, int qty);

    }
}
