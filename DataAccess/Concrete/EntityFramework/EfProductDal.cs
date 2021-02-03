using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet: .NETCore içinde EntityFrameWork geliyor
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            using (NorthWindContext context=new NorthWindContext())// using içindeki nesneler garbage collector beklemez. bellekten işi bitince hemen atılır. şart değil ama performans artırır.
            {
                var addedEntity = context.Entry(entity); //referansı yakala
                addedEntity.State = EntityState.Added; //o eklenecek nesne
                context.SaveChanges(); //ekle
            };
        }

        public void Delete(Product entity)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted; 
                context.SaveChanges();
            };
        }

        public Product Get(Expression<Func<Product, bool>> filter)//tek data getirecek
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            };
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)//filter için default olarak null verdik. kullanılmadığında null olarak atadık
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                //Eğer filtre göndermemişse tüm veriyi getir. filtre vermişse bunu filtreler
                return filter == null 
                    ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var updatedEntity = context.Entry(entity); 
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            };
        }
    }
}
