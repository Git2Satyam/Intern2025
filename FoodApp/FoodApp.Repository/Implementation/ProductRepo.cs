using FoodApp.Core.DB_Context;
using FoodApp.Models;
using FoodApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private FoodAppContext _context;
        public ProductRepo(FoodAppContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductModel> GetProductDetails()
        {
            try
            {
                var products = _context.Products.Select(x => new ProductModel
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    ProductDescription = x.ProductDescription,
                    ImageURL = x.ImageURL,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Currency = x.Currency,
                }).ToList();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
