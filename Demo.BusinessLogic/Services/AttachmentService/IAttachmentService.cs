using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        public string Upload(IFormFile formFile , string FolderName);
        public bool Delete(string filePath);
    }
}
