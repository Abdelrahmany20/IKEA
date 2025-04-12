using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Common.Services.Attachments
{
  public  interface IAttachmentServices
    {

         public string UploadImage (IFormFile File, string FolderName);

        public bool Delete(string FilePath);
    }
}
