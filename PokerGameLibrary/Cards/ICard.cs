using PokerGameLibrary.Cards.Enums;

namespace PokerGameLibrary.Cards
{
    /// <summary>
    /// Representes 2 main compounds of the game card: value and suit.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Storages the suit of the card (Diamonds, Hearts...).
        /// </summary>
        CardSuit CardSuit { get; set; }
        /// <summary>
        /// Storages the value of the card (A, 10, J...).
        /// </summary>
        CardValue CardValue { get; set; }
    }
}
