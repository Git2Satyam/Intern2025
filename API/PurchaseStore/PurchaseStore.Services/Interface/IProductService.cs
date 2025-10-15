using PurchaseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Interface
{
    public interface IProductService
    {
        List<ProductModel> GetAllProducts();
    }
}
