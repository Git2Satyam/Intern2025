using Microsoft.AspNetCore.Http;
using PurchaseStore.Core.DB_Context;
using PurchaseStore.Core.Entities;
using PurchaseStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Repository.Implementation
{
    public class ImageRepo: Repository<Product>, I_ImageRepo
    {
        PurchaseAppContext _context
        {
            get
            {
                return _dbContext as PurchaseAppContext;
            }
        }

        public ImageRepo(PurchaseAppContext _db) : base(_db)
        {
        }

        public bool SaveImage(IFormFile file, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
