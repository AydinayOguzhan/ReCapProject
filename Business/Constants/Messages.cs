using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //Cars
        public static string CarAdded = "Araba eklendi";
        public static string CarDeleted = "Araba silindi";
        public static string CarUpdated = "Araba güncellendi";
        public static string CarDescriptionInvalid = "Açıklama en az 2 karakter olmalıdır";
        public static string CarDailyPriceInvalid = "Günlük ücret 0'ın altında olamaz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Araçlar listelendi";
        public static string CarListed = "Araç listelendi";

        //Colors
        public static string ColorAdded = "Renk eklendi";
        public static string ColorDeleted = "Renk silindi";
        public static string ColorUpdated = "Renk güncellendi";
        public static string ColorsListed = "Renkler listelendi";
        public static string ColorListed = "Renk listelendi";

        //Brands
        public static string BrandAdded = "Marka eklendi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandUpdated = "Marka güncellendi";
        public static string BrandsListed = "Markalar listelendi";
        public static string BrandListed = "Marka listelendi";

        //Users
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserListed = "Kullanıcı listelendi";

        //Customers
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomersListed = "Müşteriler listelendi";
        public static string CustomerListed = "Müşteri listelendi";

        //Rentals
        public static string RentedCar = "Araç kiralandı";
        public static string RentalDeleted = "Kiralama bilgisi silindi";
        public static string RentalUpdated = "Kiralama bilgisi güncellendi";
        public static string RentalsListed = "Kiralama bilgileri listelendi";
        public static string RentalListed = "Kiralama bilgisi listelendi";
        public static string InvalidReturnDate = "Araç dönüş tarihi kiralamaya uygun değildir";
        
        
        public static string CarPhotoLimitExceded = "Araç başına sadece 5 adet fotoğraf ekleyebilirsiniz";
        public static string ThereIsNoPhoto = "Böyle bir fotoğraf bulunmamaktadır";
    }
}
