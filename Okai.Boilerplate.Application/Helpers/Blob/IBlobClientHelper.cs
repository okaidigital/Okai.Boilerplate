namespace Okai.Boilerplate.Application.Helpers.Blob
{
    public interface IBlobClientHelper
    {
        Task UploadBlobAsync(string containerName, string fileName, string content);
        public string? GetBlobContentAsync(string containerName, string fileName);
        T? GetBlobContentAsync<T>(string containerName, string fileName) where T : class;
        Task DeleteBlobContentAsync(string containerName, string fileName);
    }
}
