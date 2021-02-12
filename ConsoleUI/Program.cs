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
            //CustomerTest();

            //TODO: ReturnDate must be null if the car hasn't been returned.

            //DateTime? returnDate = null;
            Rental rental = new Rental() { CarId = 3, CustomerId = 1, RentDate = DateTime.Now};

            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(rental);
            Console.WriteLine(result.Message);

            //var result = rentalManager.GetRentalDetailByCarId(2);
            //Console.WriteLine(result.Message);
            //Console.WriteLine(result.Data.ReturnDate);

        }

        private static void CustomerTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

            var result = customerManager.GetCustomerDetailByLastName("Aydınay");
            Console.WriteLine(result.Data.CompanyName + " : " + result.Data.FirstName + " : " + result.Data.LastName);

            //var result = customerManager.GetAllCustomerDetail();
            //Console.WriteLine(result.Message);
            //foreach (var item in result.Data)
            //{
            //    Console.WriteLine($"First Name: {item.FirstName} - Last Name: {item.LastName} - Company Name: {item.CompanyName}");
            //}
        }

        private static void BrandTest()
        {
            //Brand brand1 = new Brand() { BrandName = "TESLA" };
            //Brand brand2 = new Brand() { Id = 9, BrandName = "Tesla" };

            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(brand1);

            //Console.WriteLine(brandManager.GetById(9).Data.BrandName);

            //brandManager.Update(brand2);

            //Console.WriteLine(brandManager.GetById(9).Data.BrandName);

            //brandManager.Delete(brand2);

            foreach (var item in brandManager.GetAll().Data)
            {
                Console.WriteLine($"Id: {item.Id} - Name: {item.BrandName}");
            }
        }

        private static void ColorTest()
        {
            //Color color1 = new Color() { ColorName = "Indian red" };
            //Color color2 = new Color() { Id = 15, ColorName = "Indian red" };

            ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(color1);

            //Console.WriteLine(colorManager.GetById(15).Data.ColorName);

            //colorManager.Update(color2);

            //Console.WriteLine(colorManager.GetById(15).Data.ColorName);

            //colorManager.Delete(color2);

            foreach (var item in colorManager.GetAll().Data)
            {
                Console.WriteLine($"Id: {item.Id} - Name: {item.ColorName}");
            }
        }

        private static void CarTest()
        {
            //Car car1 = new Car() { BrandId = 3, ColorId = 5, ModelYear = 1934, DailyPrice = 150, Description = "Car Description 0" };
            //Car newCar = new Car() { Id = 11,BrandId = 4, ColorId = 6, ModelYear = 1934, DailyPrice = 150, Description = "Updated Car Description 0" };

            CarManager carManager = new CarManager(new EfCarDal());

            //var result = carManager.Add(car1);
            //Console.WriteLine(result.Message);

            //carManager.Update(newCar);

            //var deleteResult = carManager.Delete(newCar);
            //Console.WriteLine(deleteResult.Message);

            foreach (var item in carManager.GetAll().Data)
            {
                Console.WriteLine($"Id: {item.Id} - Açıklama: {item.Description}");
            }


            var car = carManager.GetById(3).Data;
            Console.WriteLine($"Car Description: {car.Description} - Brand Id: {car.BrandId} - Color Id: {car.ColorId}" +
                    $" - Daily Price: {car.DailyPrice} - Model Year: {car.ModelYear}");
        }
    }
}
