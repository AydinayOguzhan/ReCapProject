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
        public CarDetailDto GetCarDetailByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = (from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             where car.Id == carId
                             select new CarDetailDto()
                             {
                                 CarId = car.Id,
                                 BrandName = brand.BrandName,
                                 CarDescription = car.Description,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 Findex = car.Findex
                             }).FirstOrDefault();

                return result;
            }
        }

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             select new CarDetailDto() {CarId = car.Id, BrandName = brand.BrandName, CarDescription = car.Description , 
                                 ColorName = color.ColorName, DailyPrice = car.DailyPrice,ModelYear=car.ModelYear,Findex=car.Findex};

                return filter == null ? result.ToList() : result.Where(filter).ToList();
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
                                 ModelYear = car.ModelYear,
                                 Findex = car.Findex
                             };

                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColorId(int colorId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             where color.Id == colorId
                             select new CarDetailDto()
                             {
                                 CarId = car.Id,
                                 BrandName = brand.BrandName,
                                 CarDescription = car.Description,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 Findex = car.Findex
                             };

                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsById(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Car
                             join brand in context.Brand
                             on car.BrandId equals brand.Id
                             join color in context.Color
                             on car.ColorId equals color.Id
                             join image in context.CarImages
                             on car.Id equals image.CarId
                             where car.Id == carId
                             select new CarDetailDto()
                             {
                                 CarId = car.Id,
                                 BrandName = brand.BrandName,
                                 CarDescription = car.Description,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 Findex = car.Findex,
                                 ImagePath = image.ImagePath,
                                 Date = image.Date
                             };

                return result.ToList();
            }
        }
    }
}
