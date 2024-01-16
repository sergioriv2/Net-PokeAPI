namespace PokeApi.Models
{
    public class S3File
    {
        public string? FileType { get; set; }
        public string? Key { get; set; }
        public string BucketName { get; set; }
        public MemoryStream InputStream { get; set; }

        public S3File()
        {
            
        }
    }
}
