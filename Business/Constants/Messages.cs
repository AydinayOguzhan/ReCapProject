using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //Cars
        public static string CarAdded = "Car added";
        public static string CarDeleted = "Car deleted";
        public static string CarUpdated = "Car updated";
        public static string CarDescriptionInvalid = "Description must be at least 2 characters";
        public static string CarDailyPriceInvalid = "Daily price can not be below 0";
        public static string MaintenanceTime = "Maintance"; 
        public static string CarsListed = "Cars listed";
        public static string CarListed = "Car listed";

        //Colors
        public static string ColorAdded = "Color added";
        public static string ColorDeleted = "Color deleted";
        public static string ColorUpdated = "Color updated";
        public static string ColorsListed = "Colors listed";
        public static string ColorListed = "Color listed";

        //Brands
        public static string BrandAdded = "Brand added";
        public static string BrandDeleted = "Brand deleted";
        public static string BrandUpdated = "Brand updated";
        public static string BrandsListed = "Brands listed";
        public static string BrandListed = "Brand listed";

        //Users
        public static string UserAdded = "User added";
        public static string UserDeleted = "User deleted";
        public static string UserUpdated = "User updated";
        public static string UsersListed = "Users listed";
        public static string UserListed = "User listed";

        //Customers
        public static string CustomerAdded = "Customer added";
        public static string CustomerDeleted = "Customer deleted";
        public static string CustomerUpdated = "Customer updated";
        public static string CustomersListed = "Customers listed";
        public static string CustomerListed = "Customer listed";

        //Rentals
        public static string RentedCar = "Car rented";
        public static string RentalDeleted = "Rental information deleted";
        public static string RentalUpdated = "Rental information updated";
        public static string RentalsListed = "Rental informations listed";
        public static string RentalListed = "Rental information listed";
        public static string InvalidReturnDate = "The return date of the vehicle is not suitable for rental";

        public static string Successful = "Successful";
        public static string Unsuccessful = "Unsuccessful";
        public static string UserNotFound = "User not found";
        public  static string PasswordError = "Wrong password";
        public static string UserAlreadyExists = "User already exist";
        public static string UserRegistered = "User created successfully";
        public static string AccessTokenCreated = "Access token created";
        public static string LoginSuccess = "Login successful";
        public static string AuthorizationDenied = "Authorization denied";


        public static string CarPhotoLimitExceded = "You can add 5 photos per car";
        public static string ThereIsNoPhoto = "Photo notBen de  found";
    }
}
