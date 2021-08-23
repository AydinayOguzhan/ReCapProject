using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class CloudinaryImageHelper
    {
        static Account account = new Account("aydinayoguzhan", "848997974779594", "vkQeKkKpthX49KB7rIGrsKnaVP0");
        static Cloudinary cloudinary = new Cloudinary(account);
        static string _path = "car_images";
        static string _notFound = "not found";

        public static IDataResult<string> UploadImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                var guidName = Guid.NewGuid().ToString();
                var type = Path.GetExtension(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(guidName + type, stream),
                        PublicId = guidName + type,
                        Folder = _path,
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    return new SuccessDataResult<String>(uploadResult.Url.ToString(),guidName + type);
                }

            }
            return new ErrorDataResult<String>();
        }

        public static IResult DeleteImage(string fileName)
        {
            var deleteParams = new DeletionParams(fileName)
            {
                PublicId =  $"{_path}/{fileName}"
            };
            var result = cloudinary.Destroy(deleteParams);
            if (result.Result == _notFound)
            {
                return new ErrorResult("Image not found");
            }
            return new SuccessResult();

        }

    }
}
