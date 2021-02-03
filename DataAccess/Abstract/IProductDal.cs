using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interfacin kendisi değil operasyonları publicdir. o yüzden interfaci de public yapdık. metodları yapmaya gerek yok. 
    public interface IProductDal : IEntityRepository<Product> //productla ilgili veritabanı operasyonlarını yapacağım interface
    {
    }
}
