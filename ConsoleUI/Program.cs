using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            Car car1 = new Car() {BrandId =3, ColorId = 5, ModelYear=1934, DailyPrice = 150, Description="Car Description 0" };
            
            CarManager carManager = new CarManager(new EfCarDal());

            //carManager.Add(car1);
            

            foreach (var item in carManager.GetAll())
            {
                Console.WriteLine($"Id: {item.Id} - Açıklama: {item.Description}");
            }

            //foreach (var item in carManager.GetCarsByBrandId(2))
            //{
            //    Console.WriteLine($"Araç açıklaması: {item.Description} -- Araç markası: {item.BrandId}");
            //}

        }
    }
}
