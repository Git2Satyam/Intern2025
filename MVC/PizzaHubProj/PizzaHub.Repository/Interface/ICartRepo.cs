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
        public int AddItemToCart(Guid cartId, int productId);

        IEnumerable<CartItemModel> GetCartItems();
        public int UpdateQuantity(string CartId, int productId, int qty);

        //public Cart CartExist(Guid cartId);

        //public Product GetProduct(int productId);
    }
}
