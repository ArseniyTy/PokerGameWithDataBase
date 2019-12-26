using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.GamePlayer;

namespace PokerGameLibrary.Bots.BotLevels
{
    /// <summary>
    /// Representes a medium bot. 
    /// <para>Implements <c>IBotLevel</c> interface.</para>
    /// <para>Strategy pattern.</para>
    /// </summary>
    public class MediumBot : IBotLevel
    {
        /// <summary>
        /// Defines the action of the bot in the current state of the game.
        /// </summary>
        /// <param name="bot">Current bot.</param>
        /// <param name="session">Current game session.</param>
        public void RoundDecision(Player bot, GameBotSession session)
        {
            new SillyBot().RoundDecision(bot, session);
        }
    }
}
