using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Implementation
{
    public class CartRepo : Repository<Cart>, ICartRepo
    {
        private FoodAppContext _context;
        public CartRepo(FoodAppContext context): base(context) 
        {
            _context = context;
        }

        public bool CartExists(Guid cartId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(x => x.Id == cartId);
                return cart != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public bool AddItemToCart(int productId, string CartId)
        //{
        //    throw new NotImplementedException();
        //}

        public ProductModel GetProduct(int productId)
        {
            try
            {
                var product = _context.Products.Where(x => x.IsDeleted == false && x.Id == productId).Select(pd => new ProductModel
                {
                    Id = pd.Id,
                    ProductName = pd.ProductName,
                    ProductDescription = pd.ProductDescription,
                    Price = pd.Price,
                    Currency = pd.Currency,
                    ImageURL = pd.ImageURL,
                    Quantity = pd.Quantity, 
                }).FirstOrDefault();
                return product;
               
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<CartItemModel> GetProducts(Guid cartId)
        {
            try
            {
                var cartExist = _context.CartItems.Where(c => c.CartId == cartId).Select(item => new CartItemModel
                {
                    id = item.Id,
                    CartId = item.CartId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Products = _context.Products.Where(x => x.IsDeleted == false && x.Id == item.ProductId).Select(pd => new ProductModel
                    {
                        Id = pd.Id,
                        ProductName = pd.ProductName,
                        ProductDescription = pd.ProductDescription,
                        Price = pd.Price,
                        Currency = pd.Currency,
                        ImageURL = pd.ImageURL,
                        Quantity = pd.Quantity,

                    }).FirstOrDefault(),
                }).ToList();
                return cartExist;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
