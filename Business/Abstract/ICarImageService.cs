using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Utilities.Helpers.FileHelper;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile file, CarImage carImage);
        IResult Update(IFormFile file, CarImage carImage);
        IResult Delete(CarImage carImage);
        IDataResult<List<CarImage>> GetById(int id);
        IDataResult<List<CarImage>> GetByCarId(int id);
        IDataResult<List<CarImage>> GetAll();

        IDataResult<CarImage> GetOneImageByCarId(int carId);
    }
}
