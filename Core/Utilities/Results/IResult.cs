using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç. voidler bir şey döndürmediği için 
    public interface IResult
    {
        bool Success { get; } //yapmaya çalıştığın işlem başarılı veya başarısız
        string Message { get; }
    }
}
