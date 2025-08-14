using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Interface
{
    public interface ICartRepo
    {
        //public bool AddItemToCart(int productId, string CartId);

        public ProductModel GetProduct(int productId);
        public bool CartExists(Guid cartId);
        
    }
}
