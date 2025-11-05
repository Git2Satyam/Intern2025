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
            bool result = false;
            try
            {
                var productExist = _context.Products.FirstOrDefault( x => x.Id == productId );
                if ( productExist != null )
                {
                    FileInfo selectDrive = new FileInfo("D:");
                    DriveInfo drive = new DriveInfo(selectDrive.Directory.Root.FullName);
                    var fullPath = Path.Combine(drive + "Internship2\\Projects\\Images\\UploadImages\\");
                    var fileName = file.FileName;

                    if (!System.IO.Directory.Exists(fullPath))
                    {
                        System.IO.Directory.CreateDirectory(fullPath);
                    }

                    var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = Path.GetExtension(fileName);
                    if(!allowedExtension.Contains(fileExtension))
                    {
                        var newExtension = fileName.Replace(fileExtension, ".png");
                        fileExtension = newExtension;
                    }
                    var imagePath = fullPath + fileName;

                    if(System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        if(stream.Length > 0)
                        {
                            BinaryReader reader = new BinaryReader(stream);
                            byte[] bytes = reader.ReadBytes((Int32)stream.Length);
                        }
                    }

                    productExist.ImageURL = imagePath;
                    _context.SaveChanges();
                    result = true;
                }
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
