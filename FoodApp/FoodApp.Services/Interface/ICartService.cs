using FoodApp.Core.Entities;
using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services.Interface
{
    public interface ICartService
    {
        public int AddItemToCart(int productId, string cartId);
        public List<CartItemModel> GetProducts(Guid cartId);
        public int UpdateQuantity(int productId, int quantity);
        public bool DeleteItem(int productId);
        CheckoutModel Checkout(int productId, string cartId);
        CheckoutModel CheckoutCheckoutForHome(int productId);

    }
}
