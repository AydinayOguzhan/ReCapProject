using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarPhotoLimit(carImage.CarId));

            if (result != null)
            {
                return new ErrorResult(Messages.Unsuccessful);
            }
            //Resim ekleme ve silme işlemini cloudinary'e taşı
            var pathResult = FileHelper.Add(file);
            carImage.ImagePath = pathResult.Message;

            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.Successful);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var result = _carImageDal.Get(c => c.Id == carImage.Id);
            var result1 = FileHelper.Update(file, result.ImagePath);
            carImage.ImagePath = result1.Message;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.Successful);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(CarImage carImage)
        {
            var result = _carImageDal.Get(c => c.Id == carImage.Id);
            FileHelper.Delete(result.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Successful);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetById(int id)
        {
            var result = _carImageDal.GetAll(c => c.Id == id);
            if (result.Count <= 0)
            {
                List<CarImage> Cimage = new List<CarImage>();
                Cimage.Add(new CarImage { CarId = id, ImagePath = @"default.jpg" });
                return new SuccessDataResult<List<CarImage>>(Cimage);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);
            if (result.Count <= 0)
            {
                List<CarImage> Cimage = new List<CarImage>();
                Cimage.Add(new CarImage { CarId = id, ImagePath = @"default.jpg" });
                return new SuccessDataResult<List<CarImage>>(Cimage);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }


        public IDataResult<CarImage> GetOneImageByCarId(int carId)
        {
            var result = _carImageDal.getByCarIdOne(carId);
            if (result == null)
            {
                CarImage image = new CarImage();
                image.CarId = carId;
                image.ImagePath = @"default.jpg";
                return new SuccessDataResult<CarImage>(image);
            }

            return new SuccessDataResult<CarImage>(result);
        }


        private IResult CheckCarPhotoLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.CarPhotoLimitExceded);
            }
            return new SuccessResult();
        }

    }
}
