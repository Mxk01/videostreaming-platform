namespace web_api.Services
{
    public interface IManageImages
    {
/* So DownloadFile method will return a Task<Tuple> 
   This task will be used to keep track of info about the  file
   such as byte[] to store info about file size,string to 
   store info about ImageName and string about image description
*/
        Task<string> UploadFile(IFormFile formFile);
        Task<(byte[], string, string)> DownloadFile(string fileName);
    }
}
