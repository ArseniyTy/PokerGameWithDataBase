using System;
using System.Collections.Generic;
using System.Linq;
using PokerGameLibrary.Classes;
using DatabaseService.Entities;
using DatabaseService.Entities.Models;


namespace DatabaseService
{
    public static class GameStatistics
    {
        public static void PlayerGameStatAdd(GameSession gameSession)
        {
            //запись в бд
            var pokerGameContext = new PokerGameContext();
            var currPlayer = gameSession.Players[0];
            var dbPlayer = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == currPlayer.Name);
            

            var gameModel = new GameSessionModel();
            gameModel.Player = dbPlayer;    // ref nav property
            gameModel.Profit = currPlayer.CurrBetMoney - dbPlayer.Money;
            gameModel.WinCondintion = gameModel.Profit > 0;
            gameModel.GameStatus = currPlayer.Status.ToString();
            gameModel.NumOfPlayers = gameSession.Players.Count;
            gameModel.GameTime = gameSession.Time;
            gameModel.Bank = gameSession.Players.Sum(p=>p.CurrBetMoney); //сумма всех текущих ставок, т.к. в конце игры текущая ставка - твой выигрыш 


            pokerGameContext.PlayerGames.Add(gameModel);
            pokerGameContext.SaveChanges();
        }

        public static List<GameSessionModel> PlayerGameStatView(Player player)
        {
            //чтение всех игр из бд (ToString() перегружен, если что)
            var pokerGameContext = new PokerGameContext();
            var dbPlayer = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == player.Name);

            pokerGameContext.Entry(dbPlayer).Collection(p => p.Games).Load(); //explicit loading
            return dbPlayer.Games;
        }
    }
}
