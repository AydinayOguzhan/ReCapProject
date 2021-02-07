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
                var result = from c in context.Car
                             join b in context.Brand
                             on c.BrandId equals b.Id
                             join co in context.Color
                             on c.ColorId equals co.Id
                             select new CarDetailDto() {CarId = c.Id, BrandName = b.BrandName, CarDescription = c.Description , 
                                 ColorName = co.ColorName, DailyPrice = c.DailyPrice};

                return result.ToList();
            }
        }

        
    }
}
