namespace PokerGameLibrary.GamePlayer.Enums
{
    /// <summary>
    /// Status of the player in the game.
    /// </summary>
    public enum PlayerStatus
    {
        Waiting, //before auction(and check)
        Fold,
        Check,
        InAuction, //bet, call, raise
        AllIn
    }
}
