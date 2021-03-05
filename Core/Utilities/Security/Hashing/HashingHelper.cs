using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {//outlar dışarıya passwordHash ve passwordSalt verir.
            //.Net hashing yapısı kullanılacak
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //burda başka bir şeyde kullanabiliriz. Buradaki key algoritmanın kendi key'i biz onu kullandık burada. Her kullanıcı için bir key oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }


        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
            
        }
    }
}
