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

        public bool DeleteProduct(int productId)
        {
            return _productRepo.DeleteProduct(productId);
        }

        public List<CategoryModel> GetAllCategories()
        {
            return _productRepo.GetAllCategories();
        }

        public List<ProductModel> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        public int InsertOrUpdateProduct(ProductModel model)
        {
            return _productRepo.InsertOrUpdateProduct(model);
        }
    }
}
