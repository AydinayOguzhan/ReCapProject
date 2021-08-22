using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, ReCapProjectContext>, ICarImageDal
    {
        public CarImage getByCarIdOne(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = (from carImage in context.CarImages
                              where carImage.CarId == carId
                              select new CarImage()
                              {
                                  CarId = carImage.CarId,
                                  Id = carImage.Id,
                                  ImagePath = carImage.ImagePath,
                                  ImageLink = carImage.ImageLink
                              }).FirstOrDefault();

                return result;
            }
        }
    }
}
