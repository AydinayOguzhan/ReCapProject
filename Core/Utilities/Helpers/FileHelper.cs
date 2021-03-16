using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        private static string currentDirectory = Environment.CurrentDirectory + @"\wwwroot";
        private static string path = @"\images\";

        public static IResult Add(IFormFile file)
        {
            if (file.Length > 0)
            {
                var guidName = Guid.NewGuid().ToString();
                var type = Path.GetExtension(file.FileName);

                using (FileStream fileStream = File.Create(currentDirectory + path + guidName + type))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                return new SuccessResult(guidName + type);
            }
            return new ErrorResult();
        }

        public static IResult Delete(string file)
        {
            File.Delete(currentDirectory + path + file);
            return new SuccessResult();
        }

        public static IResult Update(IFormFile file, string imagePath)
        {
            if (file.Length > 0)
            {
                var guidName = Guid.NewGuid().ToString();
                var type = Path.GetExtension(file.FileName);
                FileHelper.Delete(imagePath);
                FileHelper.Add(file);

                return new SuccessResult();
            }
            return new ErrorResult();
        }

    }
}
