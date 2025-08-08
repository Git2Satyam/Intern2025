using Microsoft.EntityFrameworkCore;
using PizzaHub.Core.DB_Context;
using PizzaHub.Core.Entities;
using PizzaHub.Models;
using PizzaHub.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private PizzaHubContext _context;
        public CartRepo(PizzaHubContext context)
        {
            _context = context;
        }

        public bool AddItemToCart(Guid cartId, int productId)
        {
            try
            {
                var cartExist = CartExist(cartId);
                var product = GetProduct(productId);
                if (cartExist == null)
                {
                    var addCart = new Cart
                    {
                        Id = cartId,
                        CreatedDate = DateTime.Now,
                        UserId = 1,  // dynamic
                        IsActive = true,
                    };

                    CartItem item = new CartItem
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Quantity = 1,  // dynamic
                        UnitPrice = product?.UnitPrice,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };

                    _context.CartItems.Add(item);
                    _context.Carts.Add(addCart);
                    _context.SaveChanges();
                    //addCart.CartItems.Add(item);
                    //_context.Carts.Add(addCart);
                    //_context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public IEnumerable<CartItemModel> GetCartItems()
        {
            try
            {
                var productItems = _context.CartItems.Where(itm => itm.IsActive == true).Select(ct => new CartItemModel
                {
                    Id = ct.Id,
                    CartId = ct.CartId,
                    UnitPrice = ct.UnitPrice,
                    Quantity = ct.Quantity,
                    Products = _context.Products.Where(x => x.Id == ct.ProductId).Select(p => new ProductModel
                    {
                        ProductName = p.ProductName,
                        ProductDescription = p.ProdcutDescription,
                        Currency = p.Currency,
                        ImageUrl = p.ImageUrl,  
                    }).ToList()
                }).ToList();
                return productItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Cart CartExist(Guid cartId)
        {
            try
            {
                var cartExist = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
                return cartExist;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private Product GetProduct(int productId)
        {
            try
            {
                var getProduct = _context.Products.FirstOrDefault(c => c.Id == productId);
                return getProduct;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
