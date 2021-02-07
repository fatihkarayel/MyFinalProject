using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interfacin kendisi değil operasyonları publicdir. o yüzden interfaci de public yapdık. metodları yapmaya gerek yok. 
    public interface IProductDal : IEntityRepository<Product> //productla ilgili veritabanı operasyonlarını yapacağım interface
    {
        //ürüne ait özel operasyonları ürün ve categoriye join atmak gibi operasyonları burada yapacağız. GetProductDetail gibi işler burada oluşturualacak
        List<ProductDetailDto> GetProductDetails();
    }
}
//code refactoring