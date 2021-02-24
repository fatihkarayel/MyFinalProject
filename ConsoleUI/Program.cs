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
            ProductTest();
            //CategoryTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            Console.WriteLine("\nCategory Id 2 olanlar");
            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
            //Console.WriteLine("\nFiyatı 5 ve 10 arasında olanlar");
            //foreach (var product in productManager.GetByUnitPrice(5, 20))
            //{
            //    Console.WriteLine(product.ProductName + " / " + product.CategoryId);
            //}
        }
    }
}
