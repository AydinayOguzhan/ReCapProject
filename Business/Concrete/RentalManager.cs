using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        IUserService _userService;
        ICustomerService _customerService;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, IUserService userService, ICustomerService customerService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _userService = userService;
            _customerService = customerService;
            _carService = carService;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(IsFindexEnough(rental), CanItBeRented(rental));
            if (result != null)
            {
                return new ErrorResult("Kiralanırken hata oluştu. Lütfen findex puanınızı ve return date i kontrol ediniz");
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentedCar);

        }

        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetAllRentalDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetAllRentalDetail());
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId), Messages.RentalsListed);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCustomer(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId), Messages.RentalsListed);
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListed);
        }

        [CacheAspect]
        public IDataResult<RentalDetailDto> GetRentalDetailByCarId(int carId)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDetailByCarId(carId), Messages.RentalListed);
        }

        [CacheRemoveAspect("IRentalService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByUserId(int userId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetailsByUserId(userId));
        }

        private IResult IsFindexEnough(Rental rental)
        {
            var customer = _customerService.GetById(rental.CustomerId).Data;
            var carFindex = _carService.GetById(rental.CarId).Data.Findex;
            if (customer.Findex < carFindex)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CanItBeRented(Rental rental)
        {
            var result = _rentalDal.GetRentalDetailByCarId(rental.CarId);

            if (result == null || result.RentDate == null || result.ReturnDate != null || rental.ReturnDate <= DateTime.Now)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

    }
}
