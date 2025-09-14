using PizzaHub.Core.Entities;
using PizzaHub.Models;
using PizzaHub.Repository.Interface;
using PizzaHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartrepo;
        public CartService(ICartRepo cartRepo)
        {
            _cartrepo = cartRepo;
        }

        public int AddItemToCart(Guid cartId, int productId)
        {
            return _cartrepo.AddItemToCart(cartId, productId);
        }

        public CheckoutModel CheckoutForCart(string cartId, int? productId)
        {
            return _cartrepo.CheckoutForCart(cartId, productId);
        }

        public bool DeleteItem(int productId)
        {
           return _cartrepo.DeleteItem(productId);
        }

        public IEnumerable<CartItemModel> GetCartItems()
        {
            try
            {
                return _cartrepo.GetCartItems();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int UpdateQuantity(string cartId, int productId, int qty)
        {
            return _cartrepo.UpdateQuantity(cartId, productId, qty);
        }
    }
}
