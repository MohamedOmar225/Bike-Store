using bike_store_2.Data;
using bike_store_2.Entities;
using System;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace bike_store_2.Services
{

    public interface IProductFileService
    {
        Task <string> SaveFile(IFormFile Imagefile , string name );
        void DeleteFile(string? fileName);
    }

    public class ProductFileService : IProductFileService
    {

        private readonly IWebHostEnvironment environment;
        public ProductFileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        // string[] AllowedFileExtintions = { ".jpg", ".png", ".jpeg" };   
        public async Task<string> SaveFile(IFormFile Imagefile , string name)
        {                                                                                
             var AllowedFileExtintions = new[] { ".jpg", ".png", ".jpeg" }; 
            //المسار الرئيسي للمشروع 
            var CurrentPath = environment.ContentRootPath;
            //المسار الذي سيتم حفظ الصورة فيه
            var path = Path.Combine(CurrentPath , "Uploads");

            //التحقق من وجود المجلد
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //var FileNames = new List<string>();

            if (Imagefile == null || Imagefile.Length == 0)
                throw new ArgumentNullException(nameof(Imagefile), "cannot be null. Please upload a valid file.");

                                          

            if (Imagefile.Length > 3 * 1024 * 1024)
                    throw new ArgumentException("File size exceeds the limit of 3MB.");

            var fileextension = Path.GetFileName(Imagefile.FileName).ToLowerInvariant();
             
            var Checkfilename = Path.GetExtension(fileextension);
            if (!AllowedFileExtintions.Contains(Checkfilename))
            {
                throw new ArgumentException($"Invalid file extension. Allowed extensions are: {string.Join(", ", AllowedFileExtintions)}");
            }
            var filename = $"{name}_{Guid.NewGuid()} {fileextension}";
            var filePath = Path.Combine(path, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Imagefile.CopyToAsync(stream);
            }
            //FileNames.Add(filename);            


            return filename;
        }





        public void DeleteFile(string? fileName)
        {
            //التحقق من وجود اسم الصورة 
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName),"File name must be provided.");

            //المسار الرئيسي للمشروع
            var UploadsFolder = Path.Combine(environment.ContentRootPath, "Uploads");
            //المسار الكامل للصورة
            var filePath = Path.Combine(UploadsFolder, fileName);
            //التحقق من وجود مسار الصورة
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Invalid file path");
            }            
            File.Delete(filePath);
        }







       

    }
}
