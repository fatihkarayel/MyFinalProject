using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success) //bu çalışınca success metodu da çalışacak. sadece message vermek istediğimizde kullanacağız. Yani message çalışınca success ide çalıştıracağız. tersi durumu istemiyoruz. isteseydik 
        {
            Message = message; //aşağıda sadece get var ama constructor da set edilebilirler. Readonly olan metodlar böyle yapılabilir. Programcı kafasına göre bir şey yapmasın diye metodu get bıraktık ama constructer içinde standart set işlemi yapıyoruz.
            
        }
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; } //sadece getter olduğu için implement edince farklı getiriyor. onu bu şekilde değiştirdik.

        public string Message { get; }
    }
}
