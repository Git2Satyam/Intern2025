using EcommApp.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PizzaHub.Repository.Interface;
using PizzaHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
           return _productRepo.GetProducts();
        }
    }
}
