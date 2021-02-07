using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; //global değişkenler alt çizgi ile veriliyor
        public InMemoryProductDal() //ctor  Bu işlem bellekde sanki DB den gelir gibi veri oluşturduk bu işlemi constructer içinde yaptık. 
        {
            _products = new List<Product> { 
                new Product {ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product {ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product {ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product {ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product {ProductId=5, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {

            // _products.Remove(product); // Bu şekilde referans tip silemezsiniz. LINQ bunun için var
            //Language Integrated Query
            // LINQ olmasaydı aşağıdaki gibi yapardık.

            /*Product productToDelete=null;
            foreach (var p in _products)
            {
                if (product.ProductId==p.ProductId)
                {
                    productToDelete = p;
                }
            }*/

            //LINQ ile böyle yazılır
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);//tek bir eleman bulmaya yarar. Buradaki p foreach de ki p ile aynı şey. ID olan aramalarda SingleOrDefault kullanılır. İki tane sonuç döndüren sorgularda hata verir.

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p=> p.CategoryId == categoryId).ToList();// where ifadesi koşula uyanları liste olarak döndürür.
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id sine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            //Bu işlemler entity framework tarafından yapılır ama bunu bilmek gerekir.
        }
    }
}
