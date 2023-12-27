using Azure.Storage.Blobs;
using Okai.Boilerplate.Application.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Okai.Boilerplate.Application.Helpers.Blob
{
    [ScopedService]
    public class BlobClientHelper : IBlobClientHelper
    {

        private readonly BlobServiceClient _blobServiceClient;

        public BlobClientHelper(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadBlobAsync(string containerName, string fileName, string content)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobFile = containerClient.GetBlobClient(fileName);
            var stream = new MemoryStream(contentBytes);

            try
            {
                var response = await blobFile.UploadAsync(stream, true);
            }
            finally
            {
                stream?.Dispose();
            }
        }

        public string? GetBlobContentAsync(string containerName, string fileName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(fileName);
                var response = blobClient.DownloadContent();
                return response.Value.Content.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T? GetBlobContentAsync<T>(string containerName, string fileName) where T : class
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(fileName);
                var response = blobClient.DownloadContent();
                var content = response.Value.Content.ToString();

                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task DeleteBlobContentAsync(string containerName, string fileName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception) { }
        }
    }
}
