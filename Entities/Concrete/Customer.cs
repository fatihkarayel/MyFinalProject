using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        public string CustomerId { get; set; } //çalışacağımız veri tabanında id string tutulduğu için
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }

    }
}
