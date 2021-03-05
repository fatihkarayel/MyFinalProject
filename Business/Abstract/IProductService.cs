using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll(); //burası daha önce List<Product> döndüren yapıdaydı biz IDataResult yaptık. hem liste döndürecek hemde sussess ve message döndürecek.
        IDataResult<List<Product>> GetAllByCategoryId(int Id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product); //burada daha önce void vardı onu sildik IResult yaptık. artık bir sonuç döndürebilecek döndürecek.
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);
    }
}
