using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGameLibrary.GamePlayer
{
    /// <summary>
    /// Representes all the methods connected with game actions and decisions
    /// </summary>
    public interface IPlayerGameActions
    {
        /// <summary>
        /// Updates the list of methods(that are game actions) that we can use in the current situation.  
        /// </summary>
        /// <param name="prevPlayer">Storages the previous active(still in the game) player.</param>
        void PossibleGameActionsMethodsUpdater(Player prevPlayer);
        /// <summary>
        /// Makes a Fold in the game.
        /// </summary>
        void Fold();
        /// <summary>
        /// Makes a Check in the game.
        /// </summary>
        void Check();
        /// <summary>
        /// Makes a Bet in the game.
        /// </summary>
        void Bet(int money);
        /// <summary>
        /// Makes a Call in the game.
        /// </summary>
        void Call();
        /// <summary>
        /// Makes a Raise in the game.
        /// </summary>
        void Raise(int money);
        /// <summary>
        /// Makes an All-In in the game.
        /// </summary>
        void AllIn();
    }
}
