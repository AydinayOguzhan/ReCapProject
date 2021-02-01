using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>() {
                new Car{Id=1,BrandId=1,ColorId=1,DailyPrice=200,ModelYear=2013,Description="Ürün açıklaması 1"},
                new Car{Id=2,BrandId=1,ColorId=2,DailyPrice=500,ModelYear=2013,Description="Ürün açıklaması 2"},
                new Car{Id=3,BrandId=1,ColorId=5,DailyPrice=100,ModelYear=2013,Description="Ürün açıklaması 3"},
                new Car{Id=4,BrandId=1,ColorId=7,DailyPrice=1000,ModelYear=2013,Description="Ürün açıklaması 4"},
                new Car{Id=5,BrandId=1,ColorId=3,DailyPrice=350,ModelYear=2013,Description="Ürün açıklaması 5"},
                new Car{Id=6,BrandId=1,ColorId=5,DailyPrice=344.99,ModelYear=2013,Description="Ürün açıklaması 6"},
                new Car{Id=7,BrandId=1,ColorId=2,DailyPrice=657,ModelYear=2013,Description="Ürün açıklaması 7"}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var result = _cars.SingleOrDefault(a => a.Id == car.Id);
            _cars.Remove(result);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Update(Car car)
        {
            var result = _cars.SingleOrDefault(a => a.Id == car.Id);
            result.BrandId = car.BrandId;
            result.ColorId = car.ColorId;
            result.DailyPrice = car.DailyPrice;
            result.Description = car.Description;
            result.ModelYear = car.ModelYear;
        }

        Car ICarDal.GetById(int id)
        {
            var result = _cars.SingleOrDefault(a => a.Id == id);
            return result;
        }
    }
}
