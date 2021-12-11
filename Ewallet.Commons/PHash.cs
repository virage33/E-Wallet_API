using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Commons
{
    public static class PHash
    {
        //Hashes the password
        public static List<byte[]> HashGenerator(string password)
        {
            byte[] passwordSalt;
            byte[] passwordHash;

            using (var hash=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            var result = new List<byte[]>();
            result.Add(passwordHash);
            result.Add(passwordSalt);
            return result;
        }

        //compares hashes
        public static bool CompareHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hash = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var genHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<genHash.Length; i++)
                {
                    if (genHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

    }
}
