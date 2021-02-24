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
        public static string ProductCountOfCategoryError ="Categorideki ürün sayısını aştınız.";
        public static string ProductUpdated="Urun güncellendi";
        public static string ProductNameAlreadyExits = "Aynı isimde başka ürün var.";
        public static string CategoryLimitExceeded ="Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        //public static string UnitPriceInvalid=;
    }
}
