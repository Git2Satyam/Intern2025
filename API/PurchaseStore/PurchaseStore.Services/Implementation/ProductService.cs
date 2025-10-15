using PurchaseStore.Models;
using PurchaseStore.Repository.Interface;
using PurchaseStore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Implementation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public List<ProductModel> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }
    }
}
