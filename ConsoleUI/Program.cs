using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Burada şimdilik bir newleme yapacağız ama ilerde buradan kurtulacağız 
            ProductManager productManager = new ProductManager( new InMemoryProductDal());
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

            
        }
    }
}
