using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public RentalDetailDto GetRentalDetailByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = (from r in context.Rentals
                              join car in context.Car
                              on r.CarId equals car.Id
                              join c in context.Customers
                              on r.CustomerId equals c.Id
                              join u in context.Users
                              on c.UserId equals u.Id
                              where r.CarId == carId
                              select new RentalDetailDto()
                              {
                                  Id = r.Id,
                                  CarId = c.Id,
                                  CompanyName = c.CompanyName,
                                  CustomerId = c.Id,
                                  DailyPrice = car.DailyPrice,
                                  Description = car.Description,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  ModelYear = car.ModelYear,
                                  UserId = u.Id,
                                  RentDate = r.RentDate,
                                  ReturnDate = (DateTime?)r.ReturnDate
                              }).FirstOrDefault();

                return result;
            }
        }
    }
}
