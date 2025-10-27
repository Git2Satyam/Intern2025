using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Interface
{
    public interface I_ImageService
    {
        bool SaveImage(IFormFile file, int productId);
    }
}
