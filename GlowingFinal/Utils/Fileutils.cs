using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontProject.Areas.Utils
{
    public class Fileutils
    {
        public static string Create(string FolderPath, IFormFile file)
        {
            var fileName = Guid.NewGuid() + file.FileName;
         
            var fullPath = Path.Combine(FolderPath, fileName);

            FileStream stream = new FileStream(fullPath, FileMode.Create);
           file.CopyTo(stream);
            stream.Close();
            return fileName;
        }
        public static void Delete(string FullPath)
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }
    }
}
