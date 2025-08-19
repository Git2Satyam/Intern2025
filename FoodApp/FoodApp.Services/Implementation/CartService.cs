using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Models;
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

        public int AddItemToCart(int productId, string cartId)
        {
            int result = 0;
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
                 result = 1;
            }
            else
            {
                var productExist = _context.CartItems.FirstOrDefault(x => x.CartId == id && x.ProductId == productId);
                if(productExist == null)
                {
                    var item = new CartItem
                    {
                        CartId = id,
                        ProductId = product.Id,
                        Quantity = 1,
                        UnitPrice = product.Price,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };
                    _context.CartItems.Add(item);
                    _context.SaveChanges();
                    result = 2;
                }
            }
            return result;
            
        }

        public List<CartItemModel> GetProducts(Guid cartId)
        {
            return _cartRepo.GetProducts(cartId);
        }
    }
}
