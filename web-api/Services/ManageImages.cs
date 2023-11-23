using Microsoft.AspNetCore.StaticFiles;
using System.Security.AccessControl;
using web_api.Helpers;

namespace web_api.Services
{
    public class ManageImages : IManageImages
    {
        // UploadFile method will receive the file from user
        // All fields from IFormFiel are available in our instance _IFormFile
        // So we can get FileName for example
        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                // FileName will be built with  FileName,CurrentDate in string format and the extension
                FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                // Common is an utility class for images.In the example below we're using it to get the path of the file with the name we created
                var _GetFilePath = Common.GetFilePath(FileName);
                // 
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }
                return FileName;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<(byte[],string,string)> DownloadFile(string FileName)
        {
            try
            {
                // getting the file path
                var _GetFilePath = Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                /* if _ContentType doesn't exist for this file then assign it */
                if (!provider.TryGetContentType(_GetFilePath,out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }

                // reads the content of a file into a byte array  (basically stores the file into an array)
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                // this will be the tuple to return  Task<(byte[],string,string)>
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
