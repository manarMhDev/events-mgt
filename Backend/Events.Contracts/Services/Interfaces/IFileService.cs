

using Events.Contracts.Dtos;

namespace Events.Contracts.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(UploadFileModel uploadFile);
    }
}
