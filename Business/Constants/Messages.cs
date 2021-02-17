using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {//static verince yenilemiyoruz. 
        //injection tekniğiyle çoklu dil desteği sağlanabilir ama burada bırakıyoruz.
        public static string ProductAdded = "Ürün eklendi."; //public olduğu için bu fiedleri büyük harfle verdik. Pr...
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string MaintenanceTime ="Sistem bakımda!";
        public static string ProductsListed ="Ürünler listelendi." ;
        //public static string UnitPriceInvalid=;
    }
}
