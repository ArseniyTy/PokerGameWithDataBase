using System;
using System.Collections.Generic;
using NUnit.Framework;
using PokerGameLibrary.Bots;

namespace PokerGameLibrary.Tests
{
    class GameSessionTesting
    {
        private GameSession _baseGameSession;

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CheckingIfThereCanBeLessThan3PlayersInOneSession()
        {
            int[] moneyOfThePlayers = { 100, 300 };
            Assert.Throws<Exception>(() => _baseGameSession = new GameBotSession(10, moneyOfThePlayers));
        }
        [Test]
        public void CheckingIfThereCanBeMoreThan9PlayersInOneSession()
        {
            int[] moneyOfThePlayers = { 100, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300, 300 };
            Assert.Throws<Exception>(() => _baseGameSession = new GameBotSession(10, moneyOfThePlayers));
        }
        [Test]
        public void CheckingIfThereCanBePlayersWithMoneyLessThanTwicedMinimumBet()
        {
            int[] moneyOfThePlayers = { 5, 300, 300, 300, 300 };
            Assert.Throws<Exception>(() => _baseGameSession = new GameBotSession(10, moneyOfThePlayers));
        }
        [Test]
        public void CheckingIfPlayersListCanBeEqualToNull()
        {
            Assert.Throws<Exception>(() => _baseGameSession = new GameBotSession(10, null));
        }


        [Test]
        [TestCase(-13,TestName ="NegativeNumber")]
        [TestCase(0, TestName = "NullNumber")]
        [TestCase(7, TestName = "NotDividedOn10")]
        public void CheckingIfMinimumBetCanDivideOn10AndBeGreaterThanZero(int minBet)
        {
            int[] moneyOfThePlayers = { 100, 300, 300, 300, 300 };
            Assert.Throws<Exception>(() => _baseGameSession = new GameBotSession(minBet, moneyOfThePlayers));
        }
    }
}
