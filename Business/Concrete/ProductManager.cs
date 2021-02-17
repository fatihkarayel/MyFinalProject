﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //[logAspect] --AOP
        //[Validate]
        //[RemovaCashe]
        //[Transaction]
        //[Performance]

        //Autofac çok iyi AOP imkanı sunuyor. Mesela tek başına IoC değil aslında. Eğer sadece IoC yapsaydık .Net in kendi container yapısı yeterli geliyor.
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //bir bilgilendirme dönmek istiyorum. işlem yapılıp yapılmadı noktasında
            //ama bir metodta sadece bir şey döndürebilsin. farklı şeyler döndürmek istiyorsan encapsulation yapacaksın.
            //business kodlar buraya yazılır
            //business kodu ayrı valiadasyon kodunu ayırmak gerekir.
            //nesnenin yapısal uyumu ile alakalı olan herşey doğrulamadır. Örneğin fiyat sıfırdan büyük olmalıdır veya stok eksi olmamalıdır gibi
            //
            //ehliyet alacaksanız ilkyardım, motor sınavından 70 almış mı gibi kontroller business kontrollerdir.
            //kredi alacak kişinin kredi notuna bakmak business kodu.

            //Validation yapısı oluşturduğumuz için aşağıdaki kısmı sildik. 17/02/2021
            ////if (product.UnitPrice <=0)
            ////{
            ////    return new ErrorResult(Messages.UnitPriceInvalid);
            ////}
            ////if (product.ProductName.Length < 2)
            ////{
            ////    //magic strings 
            ////    return new ErrorResult(Messages.ProductNameInvalid);
            ////}

            //ValidationTool.Validate(new ProductValidator(), product);

            _productDal.Add(product); //productDal void kalmaya devam edecek

            
            return new SuccessResult(Messages.ProductAdded);//void yerine IResult yaptım artık return ile bir şey döndürebiliyoruz.
            
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //yetkisi varmı
            //IProductDal productDal = new IProductDal başka bir manager sınıf varsa new lenmez
            if (DateTime.Now.Hour == 1) //22:00 ile 22:59 bakım zamanı
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
