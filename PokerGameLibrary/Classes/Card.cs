using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.Enums;
using PokerGameLibrary.Interfaces;

namespace PokerGameLibrary.Classes
{
    /// <summary>
    /// Representes a game card.
    /// <para>Implements interfaces: <c>ICard</c>, <c>IComparable</c>.</para>
    /// </summary>
    public class Card : ICard, IComparable<ICard>
    {
        /// <summary>
        /// Storages the suit of the card (Diamonds, Hearts...).
        /// </summary>
        public CardSuit CardSuit { get; set; }
        /// <summary>
        /// Storages the value of the card (A, 10, J...).
        /// </summary>
        public CardValue CardValue { get; set; }


        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="cardSuit">The suit of the card. Enum type.</param>
        /// <param name="cardValue">The value of the card. Enum type.</param>
        public Card(CardSuit cardSuit, CardValue cardValue)
        {
            CardSuit = cardSuit;
            CardValue = cardValue;
        }


        /// <summary>
        /// Compares 2 cards based on their values.
        /// </summary>
        /// <param name="other">The card to compare with.</param>
        /// <returns></returns>
        public int CompareTo(ICard other)
        {
            if(other!=null)
            {
                if (CardValue > other.CardValue)
                    return -1;
                else if (CardValue == other.CardValue)
                    return 0;
                else
                    return 1;
            }
            else
                throw new Exception("Comparing object equals to null");
        }
    }
}
