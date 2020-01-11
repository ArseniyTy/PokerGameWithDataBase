using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace DatabaseService.Entities.Models
{
    /// <summary>
    /// Model that describes Player.
    /// <para>One-to-many link with PlayerModel (principle entity)</para>
    /// </summary>
    public class PlayerModel : IUserSecurePassword
    {
        #region IUserSecurePassword
        //глобальная соль (для шифрования)
        public static readonly string globalSalt = "fiweji31941934joj9dfosljfolsjk324jh23j4hjhnwejhwjh8329584567838ajhef9823y4837fyweachujaehf23784f3y80ffhjashf";

        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        #endregion





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


        public string MoneyStr { get; set; }
        private BigInteger _money;

        public BigInteger Money
        {
            get { return _money; }
            set
            {
                if (value<0)
                    throw new Exception("Amount of money should be a positive number (>0)");
                _money = value;
            }
        }



        //One-to-many in principle entity
        public List<GameSessionModel> Games { get; set; }
    }
}
