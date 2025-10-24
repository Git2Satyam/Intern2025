using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<SubCategoryModel>? SubCategory { get; set; }
    }

    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
