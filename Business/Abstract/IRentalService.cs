using Core.Utilities.Results;
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
        Result Add(Rental rental);
        Result Update(Rental rental);
        Result Delete(Rental rental);
    }
}
