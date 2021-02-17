using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   //Bunun adı ATTRIBUTE Javada ANNOTATION. Bir imza aslında bu classın ne görevi göreceğini belirtiyor. ona göre yapılandırılıyor

    //IoC Container --Inversion of Control --referans tutucu bizim için sınıfları newleyip tutar. bizde interfaci buradan çağırabiliriz. Web api altında startup dosyası içinde bunu configure ederiz.
    public class ProductsController : ControllerBase
    {
        IProductService _productService;//iyi kodda önce field oluşturuyoruz loosly coupled deniyor yani bağımlılık soyuta oluyor.

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Swagger şeklinde dökümantasyonlar var
            //iyi kodda
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result); //Statu 200 Ok datanın kendisi 
            }
            return BadRequest(result);//Statu 400 Bad request döner 

            //bu  kötü bir kod dependency chain yaratıyor
            //IProductService productService = new ProductManager(new EfProductDal());
            //var result = productService.GetAll();
            //return result.Data;
            //kötü kod 2.
            //return new List<Product>
            //{
            //    new Product{ProductId=1, ProductName="Elma"},
            //    new Product{ProductId=1, ProductName="Armut"},
            //};
            //kötü kod 1. 
            //return "Merhaba";//buna ulaşmak için localhost:xxxxx/api/products yazılmalı. portu görmek için bu satıra break koy açılan explorer penceresinden görebilirsin
        }
        [HttpGet("getbyid")] //https://localhost:44357/api/products/getbyid?id=1 şeklinde çağrılır.
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);//result.Message da yapabilrdik
        }


        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
