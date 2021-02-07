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
            //CarTest();
            //ColorTest();
            //BrandTest();

        }

        private static void BrandTest()
        {
            Brand brand1 = new Brand() { BrandName = "TESLA" };
            Brand brand2 = new Brand() { Id = 9, BrandName = "Tesla" };

            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(brand1);

            Console.WriteLine(brandManager.GetById(9).BrandName);

            brandManager.Update(brand2);

            Console.WriteLine(brandManager.GetById(9).BrandName);

            brandManager.Delete(brand2);

            foreach (var item in brandManager.GetAll())
            {
                Console.WriteLine($"Id: {item.Id} - Name: {item.BrandName}");
            }
        }

        private static void ColorTest()
        {
            Color color1 = new Color() { ColorName = "Indian red" };
            Color color2 = new Color() { Id = 15, ColorName = "Indian red" };

            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(color1);

            Console.WriteLine(colorManager.GetById(15).ColorName);

            colorManager.Update(color2);

            Console.WriteLine(colorManager.GetById(15).ColorName);

            colorManager.Delete(color2);

            foreach (var item in colorManager.GetAll())
            {
                Console.WriteLine($"Id: {item.Id} - Name: {item.ColorName}");
            }
        }

        private static void CarTest()
        {
            Car car1 = new Car() { BrandId = 3, ColorId = 5, ModelYear = 1934, DailyPrice = 150, Description = "Car Description 0" };
            Car newCar = new Car() { BrandId = 3, ColorId = 6, ModelYear = 1934, DailyPrice = 150, Description = "Updated Car Description 0" };

            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Add(car1);

            carManager.Update(newCar);

            carManager.Delete(car1);

            foreach (var item in carManager.GetAll())
            {
                Console.WriteLine($"Id: {item.Id} - Açıklama: {item.Description}");
            }

            var car = carManager.GetById(3);
            Console.WriteLine($"Car Description: {car.Description} - Brand Id: {car.BrandId} - Color Id: {car.ColorId}" +
                    $" - Daily Price: {car.DailyPrice} - Model Year: {car.ModelYear}");
        }
    }
}
