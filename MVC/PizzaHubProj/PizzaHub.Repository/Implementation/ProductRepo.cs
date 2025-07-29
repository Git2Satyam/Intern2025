using EcommApp.Models;
using PizzaHub.Core.DB_Context;
using PizzaHub.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private PizzaHubContext _context;
        public ProductRepo(PizzaHubContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            try
            {
                var products = _context.Products.Where(x => x.Enabled == true).Select(pd => new ProductModel
                {
                    ProductName = pd.ProductName,
                    ProductDescription = pd.ProdcutDescription,
                    UnitPrice = pd.UnitPrice,
                    Quantity = pd.Quantity,
                    Currency = pd.Currency,
                    ImageUrl = pd.ImageUrl,
                }).ToList();
                return products;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
