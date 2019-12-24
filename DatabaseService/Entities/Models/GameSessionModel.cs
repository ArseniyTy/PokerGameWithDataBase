using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.Entities.Models
{
    public class GameSessionModel
    {
        public int Id { get; set; }
        public bool WinCondintion { get; set; }
        public string GameStatus { get; set; }
        public int NumOfPlayers { get; set; }
        public int GameTime { get; set; }
        public int Bank { get; set; }
        public int Profit { get; set; }

        public override string ToString()
        {
            return String.Format("WinCondition: {0}, Status: {1}, NumOfPlayers: {2}, GameTime: {3}, Bank: {4}, Profit {5}", 
                WinCondintion, GameStatus, NumOfPlayers, GameTime, Bank, Profit);
        }


        //One-to-many in dependent entity
        public string PlayerName { get; set; }
        public PlayerModel Player { get; set; }
    }
}
