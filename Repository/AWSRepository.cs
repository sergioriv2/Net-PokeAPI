using Amazon.S3;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Repository
{
    public class AWSRepository : IAWSRepository
    {

        private readonly IAmazonS3 _s3Client;

        public AWSRepository(IAmazonS3 s3Client)
        {
            this._s3Client = s3Client;
        }

        public async Task<string> UploadS3Image(S3File fileToUpload)
        {
            try
            {
                var response = await _s3Client.PutObjectAsync(
                        new Amazon.S3.Model.PutObjectRequest()
                        {
                            Key = fileToUpload.Key,
                            BucketName = fileToUpload.BucketName,
                            InputStream = fileToUpload.InputStream,
                            CannedACL = S3CannedACL.PublicRead,
                            ContentType = fileToUpload.FileType
                        }
                    );

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://{fileToUpload.BucketName}.s3.amazonaws.com/{fileToUpload.Key}";
                }
                else
                {
                    throw new Exception("ErrorUploading");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
