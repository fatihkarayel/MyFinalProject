using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //O: Open-closed prinsiple derki mevcut koduna bir özellik ekliyorsan hiç bir kodu değiştirmezsin.
    //Entity framework ekledik ve kodumuz hala çalışıyor.
    class Program
    {
        static void Main(string[] args)
        {
            //Burada şimdilik bir newleme yapacağız ama ilerde buradan kurtulacağız 
            ProductManager productManager = new ProductManager( new EfProductDal());
            Console.WriteLine("\nCategory Id 2 olanlar");
            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }
            Console.WriteLine("\nFiyatı 5 ve 10 arasında olanlar");
            foreach (var product in productManager.GetByUnitPrice(5,20))
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }
}
