using PurchaseStore.Core.DB_Context;
using PurchaseStore.Core.Entities;
using PurchaseStore.Models;
using PurchaseStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Repository.Implementation
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        PurchaseAppContext _context
        {
            get
            {
                return _dbContext as PurchaseAppContext;
            }
        }

        public ProductRepo(PurchaseAppContext _db) : base(_db)
        {
        }
        public List<ProductModel> GetAllProducts()
        {
            try
            {
                var product = _context.Products.Where(pd => pd.Enabled == true).Select(prd => new ProductModel
                {
                    Id = prd.Id,
                    ProductDescription = prd.ProductDescription,
                    ProductName = prd.ProductName,
                    UnitPrice = prd.UnitPrice,
                    Quantity = prd.Quantity,
                    ImageData = GetImageFile(prd.ImageURL)
                }).ToList();
                return product;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static byte[] GetImageFile(string imagePath)
        {
            if (!System.IO.File.Exists(imagePath)) return null;
            var imageByte = System.IO.File.ReadAllBytes(imagePath);
            return imageByte;
        }
    }
}
