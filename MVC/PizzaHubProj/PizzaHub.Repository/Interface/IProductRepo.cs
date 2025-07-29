using EcommApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Interface
{
    public interface IProductRepo
    {
        IEnumerable<ProductModel> GetProducts();
    }
}
