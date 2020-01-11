using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseService.Entities;
using DatabaseService.Entities.Models;
using PokerGameLibrary;
using PokerGameLibrary.GamePlayer;
using PokerGameLibrary.GamePlayer.Enums;
using System.Numerics;

namespace DatabaseService
{
    /// <summary>
    /// Static class that contains basic methods to work with game statistics data.
    /// <para>Works with database.</para>
    /// </summary>
    public static class GameStatistics
    {
        /// <summary>
        /// Updates the respectively player in database
        /// </summary>
        /// <param name="playerToUpdate">Player to update</param>
        public static void PlayerDatabaseUpdater(Player playerToUpdate)
        {
            using var pokerGameContext = new PokerGameContext();
            var dbPlayer = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == playerToUpdate.Name);

            Console.WriteLine(playerToUpdate.Money);
            dbPlayer.Money = playerToUpdate.Money;
            dbPlayer.MoneyStr = playerToUpdate.Money.ToString();
            pokerGameContext.SaveChanges();
        }


        /// <summary>
        /// PUTS player game statistic in database.
        /// </summary>
        /// <param name="gameSession">Player's current game session</param>
        /// <param name="player">Player, which game statistic we want to update</param>
        public static void PlayerGameStatAdd(GameSession gameSession, Player player)
        {
            using var pokerGameContext = new PokerGameContext();
            var currPlayer = player;
            var dbPlayer = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == currPlayer.Name);
            dbPlayer.Money = BigInteger.Parse(dbPlayer.MoneyStr);
            

            var gameModel = new GameSessionModel();
            gameModel.Player = dbPlayer;    // ref nav property
            gameModel.Profit = currPlayer.Money - dbPlayer.Money;
            gameModel.ProfitStr = gameModel.Profit.ToString();
            gameModel.WinCondintion = gameModel.Profit > 0;
            //если waiting, то значит победил в аукционе (потом просто обновление было)
            if(currPlayer.Status==PlayerStatus.Waiting)
                gameModel.Status = PlayerStatus.InAuction.ToString();
            else
                gameModel.Status = currPlayer.Status.ToString();

            gameModel.NumOfPlayers = gameSession.Players.Count;
            gameModel.GameTime = gameSession.Time;
            gameModel.Bank = 0;
            foreach (var p in gameSession.Players)//сумма всех текущих ставок, т.к. в конце игры текущая ставка - твой выигрыш
                gameModel.Bank += p.CurrBetMoney;
            gameModel.BankStr = gameModel.Bank.ToString();

            pokerGameContext.PlayerGames.Add(gameModel);
            pokerGameContext.SaveChanges();
        }

        /// <summary>
        /// GETS player game statistic from database.
        /// </summary>
        /// <param name="player">Player, which game statistic we want to view</param>
        /// <returns>List of player games</returns>
        public static List<GameSessionModel> PlayerGameStatView(Player player)
        {
            using var pokerGameContext = new PokerGameContext();
            var dbPlayer = pokerGameContext.Players
                .FirstOrDefault(p => p.Name == player.Name);
            dbPlayer.Money = BigInteger.Parse(dbPlayer.MoneyStr);

            pokerGameContext.Entry(dbPlayer).Collection(p => p.Games).Load(); //explicit loading
            return dbPlayer.Games;
        }
    }
}
