using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car() { Id = 9, BrandId = 1, ColorId = 3,DailyPrice=3500, ModelYear=2020, Description="Car Description" };
            CarManager carManager = new CarManager(new InMemoryCarDal());
            foreach (var item in carManager.GetAll())
            {
                Console.WriteLine(item.Description);
            }

            carManager.Delete(car);
            carManager.Add(car);
            carManager.Update(car);
            Console.WriteLine(carManager.GetById(3).Description);
        }
    }
}
