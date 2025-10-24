using PurchaseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Repository.Interface
{
    public interface IProductRepo
    {
        List<ProductModel> GetAllProducts();
        List<CategoryModel> GetAllCategories();
        int InsertOrUpdateProduct(ProductModel model);
        bool DeleteProduct(int productId);

    }
}
