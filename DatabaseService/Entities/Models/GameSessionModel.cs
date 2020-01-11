using System;
using System.Text;
using System.Numerics;

namespace DatabaseService.Entities.Models
{
    /// <summary>
    /// Model that describes game statistic of the Player.
    /// <para>One-to-many link with PlayerModel (dependent entity)</para>
    /// </summary>
    public class GameSessionModel
    {
        public int Id { get; set; }
        public bool WinCondintion { get; set; }
        public int NumOfPlayers { get; set; }
        public int GameTime { get; set; }
        public string BankStr { get; set; }
        public BigInteger Bank { get; set; }
        public string ProfitStr { get; set; }
        public BigInteger Profit { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return String.Format("WinCondition: {0}, Status: {1}, NumOfPlayers: {2}, GameTime: {3} s, Bank: {4}, Profit {5}", 
                WinCondintion, Status, NumOfPlayers, GameTime, BankStr, ProfitStr);
        }


        //One-to-many in dependent entity
        public string PlayerName { get; set; }
        public PlayerModel Player { get; set; }
    }
}
