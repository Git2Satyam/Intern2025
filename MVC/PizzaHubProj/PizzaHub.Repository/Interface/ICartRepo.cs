using PizzaHub.Core.Entities;
using PizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Interface
{
    public interface ICartRepo
    {
        public bool AddItemToCart(Guid cartId, int productId);

        IEnumerable<CartItemModel> GetCartItems();

        //public Cart CartExist(Guid cartId);

        //public Product GetProduct(int productId);
    }
}
