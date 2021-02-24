using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {//bu sınıf bir utiliy olduğu için çıplak kalabilir. Aşırı dizayna gerek yok.
        //iş kurallarını çalıştıracağımız bir motor
        public static IResult Run(params IResult[] logics) //virgülle istediğimiz kadar parametre gönderebiliriz.
        {
            foreach (var logic in logics)
            {
                if (!logic.Success) //kurala uymayan varsa
                {
                    return logic; //o kuralı döndür.
                }           
            }
            return null;
        }
    }
}
