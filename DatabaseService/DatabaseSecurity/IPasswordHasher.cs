using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.DatabaseSecurity
{
    /// <summary>
    /// Helps to hash password and check correctness of the password, based on hash 
    /// </summary>
    interface IPasswordHasher
    {
        /// <summary>
        /// Computes the hash of the password based on local salt + loop-through SHA512 algotithm
        /// </summary>
        /// <param name="password">Input password</param>
        /// <param name="salt">Reference on user salt</param>
        /// <returns>String representation of hash of the password</returns>
        (string hash, string salt) HashPassword(string password, string userSalt = null, string globalSalt = null);


        /// <summary>
        /// Verify whether input password is correct  
        /// </summary>
        /// <param name="password">Input password</param>
        /// <param name="passwordHashToComp">User password hash</param>
        /// <param name="salt">User salt</param>
        /// <returns>True if verification was successfully completed and false otherwise</returns>
        bool PasswordVerification(string password, string passwordHashToComp, string salt, string globalSalt);
    }
}
