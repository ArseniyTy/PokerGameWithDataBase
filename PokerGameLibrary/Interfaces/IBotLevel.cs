using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.Classes;
using PokerGameLibrary.Classes.Bots;

namespace PokerGameLibrary.Interfaces
{
    /// <summary>
    /// Representes a bot level of thinking in the game. 
    /// <para>Strategy pattern.</para>
    /// </summary>
    public interface IBotLevel
    {
        /// <summary>
        /// Defines the action of the bot in the current state of the game.
        /// </summary>
        /// <param name="bot">Current bot.</param>
        /// <param name="session">Current game session.</param>
        void RoundDecision(Player bot, GameBotSession session);
    }
}
