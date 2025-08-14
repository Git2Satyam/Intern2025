using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services.Interface
{
    public interface ICartService
    {
        public bool AddItemToCart(int productId, string cartId);
    }
}
