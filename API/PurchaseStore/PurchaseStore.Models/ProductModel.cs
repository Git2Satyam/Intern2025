using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? Currency { get; set; }

        public string? ImageURL { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[]? ImageData { get; set; }
        public bool? Enabled { get; set; }
        public int? SubCatId { get; set; }
        public int? CategoryId { get; set; }
    }
}
