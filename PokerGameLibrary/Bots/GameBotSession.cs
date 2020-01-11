using System;
using PokerGameLibrary.Bots.BotLevels;
using PokerGameLibrary.GamePlayer.Enums;
using PokerGameLibrary.GamePlayer;
using System.Numerics;

namespace PokerGameLibrary.Bots
{
    /// <summary>
    /// Represents offLine game session with bots.
    /// <para>Inherits from <c>GameSession</c></para>
    /// </summary>
    public class GameBotSession : GameSession
    {
        /// <summary>
        /// Storages the player that is controlled by the user.
        /// </summary>
        public Player myPlayer;
        //отвечает за то, когда _myPlayer принял решение и следовательно можно делать последующие действия
        /// <summary>
        /// Equals to true, when all the actions before the user decision have already been done.
        /// </summary>
        public bool WaitingForMyPlayer { get; set; }



        /// <summary>
        /// Storages the level of the bots we are playing with.
        /// <para>Strategy pattern.</para>
        /// </summary>
        private IBotLevel _botLevel;
        /// <summary>
        /// Sets the level of bots.
        /// <para>Strategy pattern.</para>
        /// </summary>
        public void SetBotLevel(IBotLevel botLevel)
        {
            if (botLevel != null)
                _botLevel = botLevel;
        }



        /// <summary>
        /// Base constructor. Has additional logic to base <c>GameSession</c> constructor.
        /// </summary>
        /// <param name="minBet">The minimum bet in the game.</param>
        /// <param name="moneyOfThePlayers">Integer array, which representes the amount of money they have</param>
        public GameBotSession(BigInteger minBet, params BigInteger[] moneyOfThePlayers) : base(minBet, moneyOfThePlayers)
        {
            myPlayer = Players[0];
            WaitingForMyPlayer = false;
            _botLevel = new SillyBot();
        }


        /// <summary>
        /// Defines logic implemented during the auction.
        /// </summary>
        protected override void Auction()
        {
            if (WaitingForMyPlayer) //перестраховка
                return;

            //проходит всех действующих игроков и смотрит, у какого ставка меньше предыдущего
            //если это бот, то делает за него решение, если игрок, то оставляет запрос
            //НО если игрок в ожидании, то значит он ходит(т.к. мы идём по порядку, то значит это
            //первый в ожидании)
            for (int i = 0; i < inGamePlayers.Count; i++)
            {
                int prInd = i - 1;
                if (i == 0)
                    prInd = inGamePlayers.Count - 1;

                if (inGamePlayers[i].Status == PlayerStatus.Waiting || (inGamePlayers[i].Status == PlayerStatus.InAuction || inGamePlayers[i].Status == PlayerStatus.Check) && inGamePlayers[i].CurrBetMoney < inGamePlayers[prInd].CurrBetMoney)
                {
                    inGamePlayers[i].PossibleGameActionsMethodsUpdater(inGamePlayers[prInd]);
                    //if (inGamePlayers[i].Equals(myPlayer))
                    if (inGamePlayers[i].AllCardsEqual(myPlayer) == 0)
                        WaitingForMyPlayer = true;
                    else
                        _botLevel.RoundDecision(inGamePlayers[i], this);
                    return;
                }
            }
        }
    }
}
