using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Interface
{
    public interface IProductRepo
    {
        IEnumerable<ProductModel> GetProductDetails();
    }
}
