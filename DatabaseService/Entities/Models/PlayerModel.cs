using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.Entities.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Money { get; set; }


        //One-to-many in principle entity
        public List<GameSessionModel> Games { get; set; }
    }
}
