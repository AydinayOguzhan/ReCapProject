using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public Result Add(Rental rental)
        {
            var rentalDetail = _rentalDal.GetRentalDetailByCarId(rental.CarId);
            if (rentalDetail != null)
            {
                if (rentalDetail.ReturnDate == null)
                {
                    return new ErrorResult(Messages.InvalidReturnDate);
                }
                else if (rentalDetail.ReturnDate >= DateTime.Now)
                {
                    return new ErrorResult(Messages.InvalidReturnDate);
                }
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentedCar);

        }

        public Result Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<List<RentalDetailDto>> GetAllRentalDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetAllRentalDetail());
        }

        public IDataResult<List<Rental>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId), Messages.RentalsListed);
        }

        public IDataResult<List<Rental>> GetByCustomer(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId), Messages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListed);
        }

        public IDataResult<RentalDetailDto> GetRentalDetailByCarId(int carId)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDetailByCarId(carId), Messages.RentalListed);
        }

        public Result Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }
    }
}
