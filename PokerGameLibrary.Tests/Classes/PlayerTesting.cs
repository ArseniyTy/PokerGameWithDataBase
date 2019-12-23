using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PokerGameLibrary.Classes;
using PokerGameLibrary.Enums;
using PokerGameLibrary.Interfaces;

namespace PokerGameLibrary.Tests.Classes
{
    class PlayerTesting
    {
        #region ApproximateCardCombinationSources
        const int basePrior = 1000000;
        //flush-royale, straight-flush ...
        static object[] _approximateCardCombinationCheckingSource10 = new[]
        {
                new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Clubs, CardValue.Ace),
                    new Card(CardSuit.Clubs, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Jack),
                    new Card(CardSuit.Clubs, CardValue.Ten)
                } }, basePrior*10 },
                new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.King)
                } }, basePrior*10 },
                new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Spades, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.King)
                } }, basePrior*10 },
                new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Spades, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.King)
                } }, basePrior*10 },
        };
        static object[] _approximateCardCombinationCheckingSource9 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Six),
                    new Card(CardSuit.Hearts, CardValue.Seven),
                    new Card(CardSuit.Hearts, CardValue.Eight)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Seven),
                    new Card(CardSuit.Hearts, CardValue.Six),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Four)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Spades, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*9 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*9 },

        };
        static object[] _approximateCardCombinationCheckingSource8 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Clubs, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*8 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Queen),
                } }, basePrior*8 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Spades, CardValue.Queen),
                } }, basePrior*8 },

        };
        static object[] _approximateCardCombinationCheckingSource7 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Queen)
                } }, basePrior*7 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Four),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Four)
                } }, basePrior*7 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Four),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Jack),
                } }, basePrior*7 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Four),
                    new Card(CardSuit.Clubs, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Queen),
                } }, basePrior*7 },
        };
        static object[] _approximateCardCombinationCheckingSource6 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*6 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Diamonds, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*6 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Three),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*6 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Three),
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*6 },
        };
        static object[] _approximateCardCombinationCheckingSource5 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*5 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*5 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Spades, CardValue.Seven),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Spades, CardValue.Two),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*5 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Nine),
                    new Card(CardSuit.Spades, CardValue.Eight),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Spades, CardValue.Seven),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*5 },
        };
        static object[] _approximateCardCombinationCheckingSource4 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                } }, basePrior*4 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Diamonds, CardValue.Queen),
                } }, basePrior*4 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Clubs, CardValue.Four),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Jack),
                    new Card(CardSuit.Diamonds, CardValue.Queen),
                    new Card(CardSuit.Diamonds, CardValue.Two)
                } }, basePrior*4 },
        };
        static object[] _approximateCardCombinationCheckingSource3 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                } }, basePrior*3 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Diamonds, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Ace),
                } }, basePrior*3 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Four),
                    new Card(CardSuit.Diamonds, CardValue.Four),
                    new Card(CardSuit.Spades, CardValue.Ace),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Ace),
                } }, basePrior*3 },
        };
        static object[] _approximateCardCombinationCheckingSource2 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                } }, basePrior*2 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Two),
                } }, basePrior*2 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ten),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Clubs, CardValue.Five),
                } }, basePrior*2 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Two),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Clubs, CardValue.Five),
                } }, basePrior*2 },
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Ace),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Hearts, CardValue.Two),
                    new Card(CardSuit.Clubs, CardValue.Five),
                } }, basePrior*2 },
        };
        static object[] _approximateCardCombinationCheckingSource1 = new[]
        {
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Diamonds, CardValue.Five),
                    new Card(CardSuit.Spades, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                } }, basePrior},
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                } }, basePrior},
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Hearts, CardValue.King),
                    new Card(CardSuit.Clubs, CardValue.Two),
                    new Card(CardSuit.Clubs, CardValue.Three)
                } }, basePrior},
            new object[] { new Player { CardList=new List<Card>
                {
                    new Card(CardSuit.Hearts, CardValue.Ace),
                    new Card(CardSuit.Hearts, CardValue.Five),
                    new Card(CardSuit.Hearts, CardValue.Queen),
                    new Card(CardSuit.Clubs, CardValue.Ten),
                    new Card(CardSuit.Clubs, CardValue.Six),
                    new Card(CardSuit.Clubs, CardValue.Two),
                    new Card(CardSuit.Hearts, CardValue.Three)
                } }, basePrior},
        };
        #endregion
        
        private Player _basePlayer;


        [SetUp]
        public void SetUp()
        {
            _basePlayer = new Player();
        }

        //В перспективе запилить тест по сравнению одинаковых комбинаций (и тогда в CardCombination() надо доделать, чтобы считались и 6-7 карты)
        [Test]
        [TestCaseSource("_approximateCardCombinationCheckingSource10")]
        [TestCaseSource("_approximateCardCombinationCheckingSource9")]
        [TestCaseSource("_approximateCardCombinationCheckingSource8")]
        [TestCaseSource("_approximateCardCombinationCheckingSource7")]
        [TestCaseSource("_approximateCardCombinationCheckingSource6")]
        [TestCaseSource("_approximateCardCombinationCheckingSource5")]
        [TestCaseSource("_approximateCardCombinationCheckingSource4")]
        [TestCaseSource("_approximateCardCombinationCheckingSource3")]
        [TestCaseSource("_approximateCardCombinationCheckingSource2")]
        [TestCaseSource("_approximateCardCombinationCheckingSource1")]
        public void CheckingCorrectnessOfApproximateDiaposonOfCardCombinationPriopirityComputation(IPlayerCards player, int approximateExpectedAnswer)
        {
            int result = player.CardCombination();
            int diff = result - approximateExpectedAnswer;
            Assert.GreaterOrEqual(diff, 0);
            Assert.Less(diff, basePrior); //разница между ответами не превосходит basePrior т.е. это цифры указыают на всё ещё одну комбинацию
        }

        [Test]
        public void CheckingIfTwoSamePlayersWithSameCardsAreEqual()
        {
            Player player1 = new Player(new List<Card> { new Card(CardSuit.Clubs, CardValue.Ace), new Card(CardSuit.Spades, CardValue.Five) }, 1000, 100, PlayerStatus.Check, PlayerRole.SmallBlind);
            Player player2 = new Player(new List<Card> { new Card(CardSuit.Clubs, CardValue.Ace), new Card(CardSuit.Spades, CardValue.Five) }, 3000, 500, PlayerStatus.AllIn, PlayerRole.None);
            Assert.AreEqual(0, player1.AllCardsEqual(player2));
        }

        [Test]
        public void CheckingIfPlayerCanHaveNegativeAmountOfMoneyToCall()
        {
            Assert.Throws<Exception>(() => Player.MoneyToCall = -1000);
        }

        [Test]
        public void CheckingIfPlayerCanHaveNegativeAmountOfMoney()
        {
            Assert.Throws<Exception>(() => _basePlayer.Money=-1000);
        }

        [Test]
        public void CheckingIfPlayerCanHaveNegativeAmountOfCurrentBetMoney()
        {
            Assert.Throws<Exception>(() => _basePlayer.CurrBetMoney = -1000);
        }

        [Test]
        public void CheckingIfPlayersCardListIsNotNull()
        {
            _basePlayer.CardList = null;
            Assert.IsNotNull(_basePlayer.CardList);
        }
    }
}
