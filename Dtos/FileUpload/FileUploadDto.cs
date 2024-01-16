using MimeDetective.Storage;

namespace PokeApi.Dtos.FileUpload
{
    public class FileUploadDto
    {
        public IFormFile FileData { get; set; }
        public string FileType { get; set; }
    }
}
