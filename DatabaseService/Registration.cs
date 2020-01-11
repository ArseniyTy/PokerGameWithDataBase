using System;
using System.Linq;
using DatabaseService.DatabaseSecurity;
using DatabaseService.Entities;
using DatabaseService.Entities.Models;
using System.Numerics;

namespace DatabaseService
{
    /// <summary>
    /// Static class that contains basic registration/authorization stage methods.
    /// <para>Works with database.</para>
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// PUTS the player with specified characteristics in database.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="money">Player money</param>
        /// <param name="password">Player password</param>
        public static void SignUp(string name, BigInteger money, string password)
        {
            using var pokerGameContext = new PokerGameContext();
            var player = new PlayerModel { Name = name, Money = money, MoneyStr = money.ToString() };

            IPasswordHasher passwordHasher = new CryptographyHelper();
            var hashAndSalt_tuple = passwordHasher.HashPassword(password, globalSalt: PlayerModel.globalSalt);
            player.PasswordHash = hashAndSalt_tuple.hash;
            player.PasswordSalt = hashAndSalt_tuple.salt;


            ///нельзя одинаковые имена - уже отслеживается, т.к. это KEY
            //if (pokerGameContext.Players
            //    .FirstOrDefault(p => p.Name == player.Name) != null)
            //{
            //    throw new Exception("The player with this name is already signed-up!");
            //}
            pokerGameContext.Players.Add(player);
            pokerGameContext.SaveChanges();
        }



        /// <summary>
        /// GETS the player with specified characteristics in database.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="password">Player password</param>
        /// <param name="money">Player money</param>
        /// <returns></returns>
        public static bool SignIn(string name, string password, out BigInteger money)
        {
            using var pokerGameContext = new PokerGameContext();
            IPasswordHasher passwordHasher = new CryptographyHelper();
            var player = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == name);
            player.Money = BigInteger.Parse(player.MoneyStr);

            if(player != null)
            {
                if(passwordHasher.
                    PasswordVerification(password, player.PasswordHash, player.PasswordSalt, PlayerModel.globalSalt))
                {
                    money = player.Money;
                    return true;
                }
            }
            money = -1;
            return false;
        }
    }
}
