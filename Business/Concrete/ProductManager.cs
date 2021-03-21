using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Aspects.Autofac.Logging;

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
        //ICategoryDal _categoryDal; //Bir entty manager kendisi hariç başka bir dal'ı enjecte edemez. Am Onun servisini enjecte edebilir.
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService) 
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }

        //[SecuredOperation("product.add,admin")]// içine yazdıklarımıza claim diyoruz. bu metod için product.add ve admin yetkilerine sahip olması gerekir.
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
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

            //örneğin bir categoride max 10 ad. ürün olmalı gibi bir kuralımız varsa o burada yazılır. Interface de yapılmaz. validation da yapılmaz.
            //validationda ise girilen verinin yapısal olarak uygun olup olmadığı kontrol edilir.


            //aşağıdaki gibi bir iş kuralını böyle yazarsan katmanlı mimarinin bir faydası olmaz. Buralar spagettiye döner. Çünkü bu iş kuralı update metodunda da uygulanması gereken bir kural
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimitExceeded());
            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            //aşağıdaki gibi yapmak yerine yukarıdaki gibi bir business motoruna hepsini gönderebiliyoruz.
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        //business kodlar
            //        _productDal.Add(product); //productDal void kalmaya devam edecek
            //        return new SuccessResult(Messages.ProductAdded);//void yerine IResult yaptım artık return ile bir şey döndürebiliyoruz.
            //    }
            //}
            //return new ErrorResult();
        }
        [CacheAspect] //key.value
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //yetkisi varmı
            //IProductDal productDal = new IProductDal başka bir manager sınıf varsa new lenmez
            if (DateTime.Now.Hour == 1) //22:00 ile 22:59 bakım zamanı
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        
        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == Id));
        }
        [CacheAspect]
        [PerformanceAspect(5)] //bu metodun çalışması 5 saniyeyi geçerse beni uyar
        [LogAspect(typeof(FileLogger))]
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
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                //business kodlar
                _productDal.Update(product); //productDal void kalmaya devam edecek
                return new SuccessResult(Messages.ProductUpdated);//void yerine IResult yaptım artık return ile bir şey döndürebiliyoruz.
            }
            return new ErrorResult();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)//Aynı isimde ürün eklenemez
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExits);
            }
            return new SuccessResult();
        }

        //eğer mevcut kategori sayısı 15 i geçtiyse sisteme yeni ürün eklenemez.
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count >15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice <10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
}
