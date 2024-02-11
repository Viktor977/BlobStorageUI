using Azure.Storage.Blobs;
using BlobStorageUI.Server.Dtos;

namespace BlobStorageUI.Server.Services
{
    public class FileService
    {
        private readonly string _storageAccount = "smallblobcontainer";
        private readonly string _accesskey = "/qUMId+zlD00dCDWMv7l3Pi0IrgBl4LQnJYys/avX27V4gWvIAumo8f6w9w2fNGegd71lIKOVahy+AStU1J/Vg==";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _filesContainer;

        public FileService()
        {
            var credential = new Azure.Storage.StorageSharedKeyCredential(_storageAccount, _accesskey);
            var blobUri = $"https://{_storageAccount}.blob.core.windows.net/";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
            _filesContainer = _blobServiceClient.GetBlobContainerClient("files");
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            List<BlobDto> files= new List<BlobDto>();

            await foreach(var file in _filesContainer.GetBlobsAsync())
            {
                string uri = _blobServiceClient.Uri.ToString();
                var name= file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto()
                {
                    Uri = uri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile blob)
        {
            BlobResponseDto responce = new BlobResponseDto();
            BlobClient client= _filesContainer.GetBlobClient(blob.FileName);

            await using(Stream data=blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            responce.Status = $"File :{blob.FileName} Upload Successfully";
            responce.Error = false;
            responce.BlobDto.Uri = client.Uri.AbsoluteUri;
            responce.BlobDto.Name = client.Name;

            return responce;
        }

        public async Task<BlobDto?>DouwnloadAsync(string blobFilename)
        {
            BlobClient file = _filesContainer.GetBlobClient(blobFilename);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFilename;
                string contentType = content.Value.Details.ContentType;

                return new BlobDto() { ContentType = contentType, Name = name, Content = blobContent };

            }

            return null;
        }

        public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            BlobClient blob = _filesContainer.GetBlobClient(blobFilename);

            await blob.DeleteAsync();

            return new BlobResponseDto() { Error = false, Status = $"File :{blobFilename} has been successfully deleted" };
        }
    }
}
