using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IAWSRepository
    {
        public Task<string> UploadS3Image(S3File fileToUpload);
    }
}
