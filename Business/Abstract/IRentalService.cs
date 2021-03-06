﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetByCustomer(int customerId);
        IDataResult<List<Rental>> GetByCarId(int carId);
        IDataResult<Rental> GetById(int id);
        IDataResult<RentalDetailDto> GetRentalDetailByCarId(int carId);
        IDataResult<List<RentalDetailDto>> GetAllRentalDetail();
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByUserId(int userId);
        IDataResult<List<RentalDetailDto>> GetRentalDetailsByCustomerId(int customerId);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
