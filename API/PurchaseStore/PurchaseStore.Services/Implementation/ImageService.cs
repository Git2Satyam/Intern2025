using Microsoft.AspNetCore.Http;
using PurchaseStore.Repository.Interface;
using PurchaseStore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Implementation
{
    public class ImageService : I_ImageService
    {
        private readonly I_ImageRepo _imageRepo;
        public ImageService(I_ImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public bool SaveImage(IFormFile file, int productId)
        {
          return _imageRepo.SaveImage(file, productId);
        }
    }
}
