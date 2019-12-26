using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.GamePlayer.Enums;
using PokerGameLibrary.GamePlayer;

namespace PokerGameLibrary.Bots.BotLevels
{
    /// <summary>
    /// Representes a very silly bot. 
    /// <para>Implements <c>IBotLevel</c> interface.</para>
    /// <para>Strategy pattern.</para>
    /// </summary>
    public class SillyBot : IBotLevel
    {
        /// <summary>
        /// Defines the action of the bot in the current state of the game.
        /// </summary>
        /// <param name="bot">Current bot.</param>
        /// <param name="session">Current game session.</param>
        public void RoundDecision(Player bot, GameBotSession session)
        {
            switch (bot.Status)
            {
                case PlayerStatus.Fold:
                case PlayerStatus.AllIn:
                    {
                        break;
                    }
                case PlayerStatus.Check:
                    {
                        bot.Call();
                        break;
                    }
                case PlayerStatus.Waiting:
                    {
                        if (Player.MoneyToCall > 0)
                        {
                            bot.Call();
                        }
                        else
                        {
                            bot.Check();
                        }
                        break;
                    }
                case PlayerStatus.InAuction:
                    {
                        bot.Call();
                        break;
                    }
            }
            Random rnd = new Random();
            System.Threading.Thread.Sleep(rnd.Next(400, 3000));
        }
    }
}
