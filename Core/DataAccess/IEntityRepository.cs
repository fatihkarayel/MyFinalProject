using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class, IEntity, new() 
        // T yi sınırlandırmak istiyoruz Generic Constraint oluştumamız gerekiyor. 
        //yani T olarak her şey gönderilmesin Neler gönderilsin 1. class olmalı. buradaki class referans tip ve 2. IEntity olmalı yada IEntity implemente eden bir nesne olmalıdır. Ayrıca IEntity nin kendisini almasın diye 3. new() lenebilir bir object şartı olsun diyoruz.
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null); // filtreler ile listeyebilmemiz için LINQ in bir metodunu kullandık.
        T Get(Expression<Func<T, bool>> filter); //listeden bir item getimemizi sağlayacak.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
