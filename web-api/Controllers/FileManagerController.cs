using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {

       private readonly IManageImages _IManageImages;
        public FileManagerController(IManageImages manageImages)
        {
            _IManageImages = manageImages;
        }

        [HttpPost]
        [Route("UploadFile")]
        [RequestSizeLimit(100 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 100 * 1024 * 1024)]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            var result = await _IManageImages.UploadFile(formFile);
            return Ok(result);
        }
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var result = await _IManageImages.DownloadFile(fileName);
            return File(result.Item1,result.Item2,result.Item2);
        }
    }
}
