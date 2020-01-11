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
        /// Computes hash of salt, if it's null.
        /// Then 1000 times computes hash from previous hash + salt hash + global salt hash.
        /// </summary>
        /// <param name="password">Users input password</param>
        /// <param name="userSalt">Field to save userSalt</param>
        /// <returns>Hash from given password</returns>
        public (string hash, string salt) HashPassword(string password, string userSalt=null, string globalSalt = null)
        {
            string globalSaltHash = "";
            if (globalSalt != null)
                globalSaltHash = GetStringHash(globalSalt);

            if (userSalt == null)
                userSalt = GetRandomSalt(20);
            string saltHash = GetStringHash(userSalt);

            string hashPassword = GetStringHash(password);
            for (int i = 0; i < 1000; i++)
            {
                hashPassword = GetStringHash(hashPassword + globalSaltHash + saltHash);
            }
            return (hash: hashPassword, salt: userSalt);
        }
        private string GetRandomSalt(int saltLength)
        {
            string symb = "abcdefghijklmnopqrstuvwxyz1234567890";
            var word = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < saltLength; i++)
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
        public bool PasswordVerification(string password, string passwordHashToComp, string salt, string globalSalt)
        {
            if (password == null || passwordHashToComp == null)
            {
                throw new Exception("Input password or users password can't be equal to null!");
            }

            if (HashPassword(password, salt, globalSalt).hash == passwordHashToComp)
                return true;
            else
                return false;
        }
    }
}
