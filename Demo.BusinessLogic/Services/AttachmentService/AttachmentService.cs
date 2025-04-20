using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {

        List<string> AllowedExtensions = [".png",".jpg",".jpeg"];

        int MaxSize = 2_097_152;   // 2 mega byte (2*1024*1024)


        public string? Upload(IFormFile file , string folderName)
        {
            if (file is null) return null;

            //1. Check Extension
            var extension=Path.GetExtension(file.FileName); //.png 
            if ( ! AllowedExtensions.Contains(extension) ) return null;

            //2. Check Size
            if(file.Length >= MaxSize ) return null;

            //3. Get Located Folder Path
            //wwwroot/files/images , wwwroot/files/videos , wwwroot/files/pdfs
            //var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files{FolderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "Files" , folderName);

            //4. Make Attachment Name Unique - GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5. Get File Path
            var filePath= Path.Combine(folderPath, fileName);

            //6. Create File Stream To Copy File [Unmanaged]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            //7. Use Stream To Copy File
            file.CopyTo(fileStream);

            //8. Return FileName To Store In Database
            return fileName;
        }

        public bool Delete(string filePath)
        {
            if( ! File.Exists(filePath) ) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }
    }
}
