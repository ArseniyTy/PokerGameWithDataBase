using System;
using System.Collections.Generic;
using PokerGameLibrary.GamePlayer.Enums;

namespace PokerGameLibrary.GamePlayer
{
    /// <summary>
    /// Representes general aspects of the player.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Storages player money.
        /// </summary>
        int Money { get; set; }
        /// <summary>
        /// Storages player current bet. It equals to null, if player hasn't bet yet.
        /// </summary>
        int CurrBetMoney { get; set; }
        /// <summary>
        /// Storages game status of the player.
        /// </summary>
        /// <value>Gets the enum <c>PlayerStatus</c>.</value>
        PlayerStatus Status { get; set; }
        /// <summary>
        /// Storages game role of the player(D, SB, BB).
        /// </summary>
        /// <value>Gets the enum <c>PlayerRole</c>.</value>
        PlayerRole Role { get; set; }
    }
}
