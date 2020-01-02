using System;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseService.DatabaseSecurity
{
    /// <summary>
    /// Contains logic to help make database storaging safe
    /// <para>Implements interfaces: <code>IPasswordHasher</code></para>
    /// </summary>
    class CryptographyHelper : IPasswordHasher
    {
        /// <summary>
        /// Sets random users salt if it's not set yet. Computes its hash. 
        /// Then 1000 times computes hash from previous hash + salt hash.
        /// </summary>
        /// <param name="password">Users input password</param>
        /// <param name="userSalt">Field to save userSalt</param>
        /// <returns>Hash from given password</returns>
        public string HashPassword(string password, ref string userSalt)
        {
            if (userSalt == null)
                userSalt = GetRandomSalt();
            string saltHash = GetStringHash(userSalt);

            string hashPassword = GetStringHash(password);
            for (int i = 0; i < 1000; i++)
            {
                hashPassword = GetStringHash(hashPassword + saltHash);
            }
            return hashPassword;
        }
        private string GetRandomSalt()
        {
            string symb = "abcdefghijklmnopqrstuvwxyz1234567890";
            var word = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                int rndNum = rnd.Next(0, symb.Length);
                word.Append(symb[rndNum]);
            }
            return word.ToString();
        }
        private string GetStringHash(string str)
        {
            SHA512 sha512Hasher = new SHA512CryptoServiceProvider();
            byte[] byteString = Encoding.Default.GetBytes(str); // переводит строку в байты
            byte[] byteHash = sha512Hasher.ComputeHash(byteString); //вычисляет хэш c помошью SHA512 этих байтов

            StringBuilder strBuilderHash = new StringBuilder();
            for (int i = 0; i < byteHash.Length; i++)
            {
                strBuilderHash.Append(byteHash[i].ToString("x2")); //переводит каждый байт хэша в 16-ричную систему и записывает её в строку
            }
            return strBuilderHash.ToString();
        }

        /// <summary>
        /// Computes hashes from 2 passwords and compares them.
        /// </summary>
        /// <param name="password">Input password</param>
        /// <param name="passwordToComp">User password</param>
        /// /// <param name="salt">User salt</param>
        /// <returns>True if input password is correct. False otherwise/</returns>
        public bool PasswordVerification(string password, string passwordHashToComp, string salt)
        {
            if (password == null || passwordHashToComp == null)
            {
                throw new Exception("Input password or users password can't be equal to null!");
            }

            if (HashPassword(password, ref salt) == passwordHashToComp)
                return true;
            else
                return false;
        }
    }
}
