using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //bir bilgilendirme dönmek istiyorum. işlem yapılıp yapılmadı noktasında
            //ama bir metodta sadece bir şey döndürebilsin. farklı şeyler döndürmek istiyorsan encapsulation yapacaksın.
            //business kodlar buraya yazılır
            if (product.ProductName.Length < 2)
            {
                //magic strings 
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            
            _productDal.Add(product); //productDal void kalmaya devam edecek

            
            return new SuccessResult(Messages.ProductAdded);//void yerine IResult yaptım artık return ile bir şey döndürebiliyoruz.
            
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //yetkisi varmı
            //IProductDal productDal = new IProductDal başka bir manager sınıf varsa new lenmez
            if (DateTime.Now.Hour==22) //22:00 ile 22:59 bakım zamanı
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId == Id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 23) //22:00 ile 22:59 bakım zamanı (Test için böyle yaptık.)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
