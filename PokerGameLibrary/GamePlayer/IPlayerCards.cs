using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.Cards;

namespace PokerGameLibrary.GamePlayer
{
    /// <summary>
    /// Representes all the aspects connected with player game cards.
    /// </summary>
    public interface IPlayerCards
    {
        /// <summary>
        /// Storages all the cards of the player.
        /// </summary>
        List<Card> CardList { get; set; }

        /// <summary>
        /// Counts the priority of the card combination, by which we can define the winner. 
        /// </summary>
        /// <returns>The priority of the card combination.</returns>
        int CardCombination();
    }
}
