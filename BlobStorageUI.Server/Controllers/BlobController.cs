using BlobStorageUI.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorageUI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly FileService fileService;

        public BlobController(FileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsunc()
        {
          var result=  await fileService.ListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await fileService.UploadAsync(file);
            return Ok(result);
        }

        [HttpGet]
        [Route("filename")]
        public async Task<IActionResult>Douwnload(string fileName)
        {
            var result = await  fileService.DouwnloadAsync(fileName);
            return File(result.Content,result.ContentType,result.Name);
        }

        [HttpDelete]
        [Route("filename")]
        public async Task<IActionResult>Delete(string fileName)
        {
            var result = await fileService.DeleteAsync(fileName);
            return Ok(result);
        }
    }
}
