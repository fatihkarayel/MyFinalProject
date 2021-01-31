using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interfacin kendisi değil operasyonları publicdir. o yüzden interfaci de public yapdık. metodları yapmaya gerek yok. 
    public interface IProductDal //productla ilgili veritabanı operasyonlarını yapacağım interface
    {
        List<Product> GetAll(); //burada product için referanslara eklenmeli. Sonra using e eklenmeli
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetAllByCategory(int categoryId); //Ürünleri Kategoriye göre filtreleyen metod 
    }
}
