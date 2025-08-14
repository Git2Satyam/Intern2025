using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Repository.Interface;
using FoodApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly FoodAppContext _context;
        public CartService(ICartRepo cartRepo, FoodAppContext context)
        {
            _cartRepo = cartRepo;
            _context = context;
        }

        public bool AddItemToCart(int productId, string cartId)
        {
            bool flag = false;
            var id = new Guid(cartId);
            var product = _cartRepo.GetProduct(productId);
            var cartExist = _cartRepo.CartExists(id);

            if (!cartExist)
            {
                var cart = new Cart
                {
                    Id = id,
                    UserId = 1, // dynamic
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                };
                CartItem item = new CartItem();
                if (product != null)
                {
                    item = new CartItem
                    {
                        CartId = id,
                        ProductId = product.Id,
                        Quantity = 1, // dynamic
                        UnitPrice = product.Price,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };
                }
                 cart.CartItem.Add(item);
                _context.Carts.Add(cart);
                _context.SaveChanges();
                flag = true;
            } 
            return flag;
            
        }
    }
}
