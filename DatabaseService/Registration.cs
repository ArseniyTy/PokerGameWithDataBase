using System;
using System.Linq;
using DatabaseService.Entities;
using DatabaseService.Entities.Models;

namespace DatabaseService
{
    public static class Registration
    {
        //нельзя одинаковые имена + это же у нас ключ => как будет реагировать бд
        public static void LogIn(string name, int money, string password)
        {
            //запись в бд
            var pokerGameContext = new PokerGameContext();
            var player = new PlayerModel { Name = name, Money = money, Password = password };
            pokerGameContext.Players.Add(player);
            pokerGameContext.SaveChanges();
        }
        public static bool SignIn(string name, string password, out int money)
        {
            //чтение из бд
            var pokerGameContext = new PokerGameContext();
            var player = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == name && p.Password==password);
            if(player != null)
            {
                money = player.Money;
                return true;
            }
            //foreach (var player in pokerGameContext.Players)
            //{
            //    if(player.Name==name && player.Password==password)
            //    {
            //        money = player.Money;
            //        return true;
            //    }
            //}
            money = -1;
            return false;
        }
    }
}
