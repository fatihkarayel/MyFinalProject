using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key); //Cachede varmı bakacak metod
        void Remove(string key); //Cache temizlenecek
        void RemoveByPattern(string pattern); //metodun içinde geçen bir kelimeye veya patterne göre çalışacak. Cache temizlenecek
    }
}
