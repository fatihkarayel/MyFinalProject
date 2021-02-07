using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context: DB tabloları ile proje classlarını bağlamaya yarar
    public class NorthWindContext:DbContext // Entity framework ile gelir
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //hangi veritabanı ile ilişkili belirteceğimiz yer 'override on' yazınca otomatik geliyor bu syntax.
        {
            //başına @ koyunca ters slaş ları tek yazabiliriz.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind; Trusted_Connection=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
