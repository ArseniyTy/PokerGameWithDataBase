using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.Entities.Models
{
    /// <summary>
    /// Model that describes Player.
    /// <para>One-to-many link with PlayerModel (principle entity)</para>
    /// </summary>
    public class PlayerModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                if (value.Length == 0 || value.Length > 15)
                    throw new Exception("Name lenght should be (0;15]");
                _name = value; 
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length < 5 || value.Length > 100)
                    throw new Exception("Password lenght should be [5;100]");
                _password = value;
            }
        }
        

        private int _money;
        public int Money
        {
            get { return _money; }
            set
            {
                if (value<=0)
                    throw new Exception("Amount of money should be a positive number (>0)");
                _money = value;
            }
        }


        //One-to-many in principle entity
        public List<GameSessionModel> Games { get; set; }
    }
}
