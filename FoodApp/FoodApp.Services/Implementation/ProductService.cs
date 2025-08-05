using FoodApp.Models;
using FoodApp.Repository.Interface;
using FoodApp.Services.Interface;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<ProductModel> GetProductDetails()
        {
            try
            {
                return _productRepo.GetProductDetails();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
