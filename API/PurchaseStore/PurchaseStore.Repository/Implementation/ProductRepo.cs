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
                    ImageData = GetImageFile(prd.ImageURL),
                    SubCatId = prd.SubCatId,
                    CategoryId = prd.SubCategory.CatId
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

        public List<CategoryModel> GetAllCategories()
        {
            try
            {
                var categories = _context.Categories.Where(x => x.Deleted == false).Select(ct => new CategoryModel
                {
                    Id = ct.Id,
                    Name = ct.Cat_Name,
                    SubCategory = ct.SubCategories.Where(c => c.Deleted == false).Select(sc => new SubCategoryModel
                    {
                        Id = sc.Id,
                        Name = sc.SubCat_Name
                    }).ToList()
                }).ToList();

                return categories;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertOrUpdateProduct(ProductModel model)
        {
            int result = 0;
            try
            {
                var productExist = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (productExist == null)
                {
                    var addProduct = new Product
                    {
                        ProductName = model.ProductName,
                        ProductDescription = model.ProductDescription,
                        Quantity = model.Quantity,
                        UnitPrice = model.UnitPrice,
                        CreatedDate = DateTime.Now,
                        Enabled = true,
                        SubCatId = model.SubCatId,
                        Currency = "₹"
                    };
                    _context.Products.Add(addProduct);
                    _context.SaveChanges();
                    result = 1;
                }
                else
                {
                    productExist.ProductName = model.ProductName;
                    productExist.ProductDescription = model.ProductDescription;
                    productExist.Quantity = model.Quantity;
                    productExist.UnitPrice = model.UnitPrice;
                    productExist.ModifiedDate = DateTime.Now;
                    productExist.SubCatId = model.SubCatId;

                    _context.Products.Update(productExist);
                    _context.SaveChanges();
                    result = 1;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteProduct(int productId)
        {
            bool flag  = false;
            try
            {
                var productExist = _context.Products.FirstOrDefault(x => x.Id ==  productId);
                if(productExist != null)
                {
                    productExist.Enabled = false;
                    _context.SaveChanges();
                    flag = true;
                }
                return flag;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
