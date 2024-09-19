using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MyDemo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UplodeFile(IFormFile file , string folderName)
        {
            //To Uplode file We should follow 4 steps

            //1. Get Loacted Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //2. Get File Name And Make It UINQUE
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            //4. Save File As Streams : [Data Per Time]
            using var fs = new FileStream(filePath , FileMode.Create);
            file.CopyTo(fs);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\files" , folderName,fileName);
           
            if(File.Exists(filePath))
                File.Delete(filePath);
        }        
    }
}
