using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public string? Currency { get; set; }

        public string? ImageURL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }
    }

}
