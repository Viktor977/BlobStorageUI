namespace BlobStorageUI.Server.Dtos
{
    public class BlobResponseDto
    {
        public BlobDto BlobDto { get; set; }

        public string? Status { get; set; }

        public bool Error { get; set; }

        public BlobResponseDto()
        {
            BlobDto = new BlobDto();
        }
    }
}
