using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MinhaVida.CodeChallege.VaccinationManagement.API.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories
{
    public class BlobRepository
    {
        private readonly IOptions<StorageOptions> opts;

        public BlobRepository(IOptions<StorageOptions> opts)
        {
            this.opts = opts;
        }

        public async Task<string> SaveAsync(Stream stream, string folder, string filename)
        {
            var container = await GetContainer();
            var blob = container.GetBlockBlobReference(folder);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob?.Uri.ToString();
        }

        private async Task<CloudBlobContainer> GetContainer()
        {
            var account = CloudStorageAccount.Parse(opts.Value.ConnectionString);
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(opts.Value.Name);
            await container.CreateIfNotExistsAsync();

            var permissions = await container.GetPermissionsAsync();

            if (permissions.PublicAccess == BlobContainerPublicAccessType.Off)
            {
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                await container.SetPermissionsAsync(permissions);
            }
            return container;
        }
    }
}
