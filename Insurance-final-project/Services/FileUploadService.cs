using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;

namespace Insurance_final_project.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;

        private readonly ICloudinary _cloudinary;

        public FileUploadService(IConfiguration configuration)
        {
            _configuration = configuration;

            Account account = new Account(_configuration.GetValue<string>("CloudinarySettings:CloudName")
                , _configuration.GetValue<string>("CloudinarySettings:ApiKey")
                , _configuration.GetValue<string>("CloudinarySettings:ApiSecret"));

            _cloudinary = new Cloudinary(account);

        }


        public async Task<FileUploadResponseDto> Uploadfile(GetFileDto fileDto)
        {

            if (fileDto.file == null || fileDto.file.Length == 0)
            {
                throw new FileNotUploadedException("Please upload file!");
            }

            const long maxFileSize = 5 * 1024 * 1024; // 5MB
            if (fileDto.file.Length > maxFileSize)
            {
                throw new  FileSizeInvalidException("File size exceeds the 5MB limit.");
            }

            // Check file extension
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(fileDto.file.FileName).ToLower();
            if (Array.IndexOf(validExtensions, fileExtension) == -1)
            {
                throw new InvalidImageTypeException("Invalid file format. Only .jpg, .jpeg, .png, .gif, .bmp files are allowed.");
            }

            var uploadResult = new ImageUploadResult();
            var file = fileDto.file;

            using(var fileStream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, fileStream)
                };
                uploadResult = _cloudinary.Upload(uploadParams);
            }

            var fileUpload = new FileUploadResponseDto()
            {
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId,
                DateTime = DateTime.UtcNow,
            };

            return fileUpload;
        }
    }
}
