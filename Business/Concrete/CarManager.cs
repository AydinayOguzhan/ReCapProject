using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperations("admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        //[SecuredOperations("Product.List")]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == id), Messages.CarListed);
        }

        public IDataResult<CarDetailDto> GetDetailsByCarId(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetailByCarId(id), Messages.CarListed);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandId(brandId), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColorId(colorId), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int carId)
        {
            //return new SuccessDataResult<List<CarDetailDto>>(CheckIfCarHasPhoto(carId), Messages.CarsListed);
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsById(carId));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.CarsListed);
        }

        [SecuredOperations("admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }


        private List<CarDetailDto> CheckIfCarHasPhoto(int carId)
        {
            string path = @"\default.png";
            var result = _carDal.GetCarDetailsById(carId).Any();
            if (!result)
            {
                var newCarDetailDto = _carDal.GetCarDetails(c => c.CarId == carId).FirstOrDefault();
                return new List<CarDetailDto> { new CarDetailDto {
                    CarId = newCarDetailDto.CarId,BrandName = newCarDetailDto.BrandName, CarDescription = newCarDetailDto.CarDescription,
                    ColorName = newCarDetailDto.ColorName, DailyPrice = newCarDetailDto.DailyPrice, Date = newCarDetailDto.Date,
                    ModelYear = newCarDetailDto.ModelYear, ImagePath = path
                } };
            }
            return _carDal.GetCarDetailsById(carId);
        }
    }
}
