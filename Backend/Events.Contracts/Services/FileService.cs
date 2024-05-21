

using Events.Contracts.Dtos;
using Events.Contracts.Enums;
using Events.Contracts.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Events.Contracts.Services
{
    public class FileService : BaseService , IFileService
    {
        
        public FileService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment)
        {
        }
        public async Task<string> UploadFile(UploadFileModel uploadFile)
        {
            var rootPath = _webHostEnvironment.ContentRootPath;
            var extension = Path.GetExtension(uploadFile.FormFile.FileName);
            var fileFullName = $"{Guid.NewGuid().ToString()}{extension}";
            ImagePath imagePath = new ImagePath(rootPath, uploadFile.FolderPrefix, fileFullName);
            await saveFileOnServer(uploadFile.FormFile, imagePath);
            return imagePath.absoluteUrl;
        }
        private async Task saveFileOnServer(IFormFile file, ImagePath imagePath)
        {
            using (var inputStream = new FileStream(imagePath.physicalPath, FileMode.Create))
            {
                // read file to stream
                await file.CopyToAsync(inputStream);
                // stream to byte array
                byte[] array = new byte[inputStream.Length];
                inputStream.Seek(0, SeekOrigin.Begin);
                inputStream.Read(array, 0, array.Length);
            }
        }
    }
}
