using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IFileUploadService
    {
        public Task<FileUploadResponseDto> Uploadfile(GetFileDto file);
    }
}
