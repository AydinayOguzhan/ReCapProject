using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalDal:IEntityRepository<Rental>
    {
        RentalDetailDto GetRentalDetailByCarId(int carId);
        List<RentalDetailDto> GetAllRentalDetail();
        List<RentalDetailDto> GetRentalDetailsByUserId(int userId);
        List<RentalDetailDto> GetRentalDetailsByCustomerId(int customerId);
    }
}
