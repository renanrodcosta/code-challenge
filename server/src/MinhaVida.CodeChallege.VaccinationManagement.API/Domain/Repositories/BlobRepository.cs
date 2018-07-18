using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace MinhaVida.CodeChallege.VaccinationManagement.API.Domain.Repositories
{
    public class BlobRepository
    {
        private readonly CloudBlobContainer container;

        public BlobRepository(CloudBlobContainer container)
        {
            this.container = container;
        }

        public async Task<string> UploadAsync(string path, string destination)
        {
            var blob = container.GetBlockBlobReference(destination);
            await blob.UploadFromFileAsync(path);
            return string.Empty;
        }
    }
}
