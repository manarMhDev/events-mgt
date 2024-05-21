

using Events.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace Events.Contracts.Dtos
{
    public class UploadFileModel
    {
        public IFormFile? FormFile { get; set; }
        public FolderType FolderPrefix { get; set; }
    }
}
