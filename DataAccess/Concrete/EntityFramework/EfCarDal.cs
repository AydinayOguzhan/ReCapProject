using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             select new CarDetailDto() {CarId = car.Id, BrandName = brand.BrandName, CarDescription = car.Description , 
                                 ColorName = color.ColorName, DailyPrice = car.DailyPrice,ModelYear=car.ModelYear};

                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             where brand.Id == brandId
                             select new CarDetailDto()
                             {
                                 CarId = car.Id,
                                 BrandName = brand.BrandName,
                                 CarDescription = car.Description,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear
                             };

                return result.ToList();
            }
        }
    }
}
