using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity: class, IEntity, new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())// using içindeki nesneler garbage collector beklemez. bellekten işi bitince hemen atılır. şart değil ama performans artırır.
            {
                var addedEntity = context.Entry(entity); //referansı yakala
                addedEntity.State = EntityState.Added; //o eklenecek nesne
                context.SaveChanges(); //ekle
            };
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            };
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)//tek data getirecek
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            };
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)//filter için default olarak null verdik. kullanılmadığında null olarak atadık
        {
            using (TContext context = new TContext())
            {
                //Eğer filtre göndermemişse tüm veriyi getir. filtre vermişse bunu filtreler
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            };
        }
    }
}
