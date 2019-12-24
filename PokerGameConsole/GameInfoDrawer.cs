using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.Enums;
using PokerGameLibrary.Classes.Bots;
using PokerGameLibrary.Interfaces;
using PokerGameLibrary.Classes;
using DatabaseService;

namespace PokerGameConsole
{
    /// <summary>
    /// Implements logic connected with drawing actions of the game session in the Console.
    /// </summary>
    public static class GameInfoDrawer
    {
        /// <summary>
        /// Sets the output encoding.
        /// </summary>
        static GameInfoDrawer()
        {
            Console.OutputEncoding = Encoding.Unicode;
        }
        /// <summary>
        /// Converts cards suits and card values to symbols. Changes the colour of the text.
        /// </summary>
        /// <param name="card">The converting card.</param>
        /// <returns>The string representation of converted symbols.</returns>
        private static string CardConverter(ICard card)
        {
            string cardToStr = "";
            switch (card.CardValue)
            {
                case CardValue.Ace:
                    {
                        cardToStr += "A";
                        break;
                    }
                case CardValue.King:
                    {
                        cardToStr += "K";
                        break;
                    }
                case CardValue.Queen:
                    {
                        cardToStr += "Q";
                        break;
                    }
                case CardValue.Jack:
                    {
                        cardToStr += "J";
                        break;
                    }
                case CardValue.Ten:
                    {
                        cardToStr += "10";
                        break;
                    }
                case CardValue.Nine:
                    {
                        cardToStr += "9";
                        break;
                    }
                case CardValue.Eight:
                    {
                        cardToStr += "8";
                        break;
                    }
                case CardValue.Seven:
                    {
                        cardToStr += "7";
                        break;
                    }
                case CardValue.Six:
                    {
                        cardToStr += "6";
                        break;
                    }
                case CardValue.Five:
                    {
                        cardToStr += "5";
                        break;
                    }
                case CardValue.Four:
                    {
                        cardToStr += "4";
                        break;
                    }
                case CardValue.Three:
                    {
                        cardToStr += "3";
                        break;
                    }
                case CardValue.Two:
                    {
                        cardToStr += "2";
                        break;
                    }
                default:
                    throw new Exception("Card value is equal to null!");
            }
            switch (card.CardSuit)
            {
                case CardSuit.Clubs:
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        cardToStr += '\u2663';
                        break;
                    }
                case CardSuit.Diamonds:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        cardToStr += '\u2666';
                        break;
                    }
                case CardSuit.Hearts:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        cardToStr += '\u2665';
                        break;
                    }
                case CardSuit.Spades:
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        cardToStr += '\u2660';
                        break;
                    }
                default:
                        throw new Exception("Card suit is equal to null!");
            }
            return cardToStr;
        }


        public static void PlayerGameStatViewConsole(Player player)
        {
            foreach (var p in GameStatistics.PlayerGameStatView(player))
            {
                Console.WriteLine(p.ToString());
            }
        }

        /// <summary>
        /// Outputs the info and game process of the game session in Console.
        /// </summary>
        /// <param name="session">Game session we want to get info from.</param>
        public static void DrawInfo(GameBotSession session)
        {
            //рисует карты стола
            Console.WriteLine("Table cards:");
            for (int i = 0; i < session.tableCards.Count; i++)
            {
                Console.WriteLine(CardConverter(session.tableCards[i]));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------------------------------------------------------");

            if(session.Finished)
            {
                //каждого игрока + справа их выигрышная инфа
                Console.WriteLine("Players:");
                for (int i = 0; i < session.Players.Count; i++)
                {
                    string message = String.Format("Player №{0}", i + 1);
                    if (session.Players[i].Role != PlayerRole.None)
                    {
                        if (session.Players[i].Role == PlayerRole.Dealer)
                            message = String.Format("{0} ({1})     \t", message, session.Players[i].Role);
                        else
                            message = String.Format("{0} ({1})     ", message, session.Players[i].Role);
                    }
                    else
                        message += "\t\t";
                    Console.Write(message);

                    //Далее всё через write(), т.к. по другому цвет карт не установишь
                    if (session.Players[i].Status != PlayerStatus.Fold)
                    {
                        //message = String.Format("{0}\t{1} {2}", message, CardConverter(session.Players[i].CardsList[0]), CardConverter(session.Players[i].CardsList[1]));
                        Console.Write("\t" + CardConverter(session.Players[i].CardList[0]));
                        Console.Write(" " + CardConverter(session.Players[i].CardList[1]));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write("\tFolded");
                    }
                    Console.Write("\tMoney: " + session.Players[i].Money);

                    if (session.Players[i].CurrBetMoney != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("(+" + session.Players[i].CurrBetMoney + ")\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine();

                }
                Console.WriteLine("------------------------------------------------------------------------------------");
            }
            else
            {
                //2 карты мои
                Console.WriteLine("Your cards:");
                Console.WriteLine(CardConverter(session.myPlayer.CardList[0]));
                Console.WriteLine(CardConverter(session.myPlayer.CardList[1]));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------------------------------");


                //каждого игрока + справа их ставка/статус
                Console.WriteLine("Players:\t     Bank: {0}", session.Bank);
                for (int i = 0; i < session.Players.Count; i++)
                {
                    string message = String.Format("Player №{0}", i + 1);
                    if (session.Players[i].Role != PlayerRole.None)
                    {
                        if (session.Players[i].Role == PlayerRole.Dealer)
                            message = String.Format("{0} ({1})     \t", message, session.Players[i].Role);
                        else
                            message = String.Format("{0} ({1})     ", message, session.Players[i].Role);
                    }
                    else
                        message += "\t\t";
                    message = String.Format("{0}\tMoney: {1}\t", message, session.Players[i].Money);
                    if (session.Players[i].Status != PlayerStatus.InAuction)
                    {
                        message = String.Format("{0}\t{1}", message, session.Players[i].Status);
                        if (session.Players[i].CurrBetMoney != 0)
                            message += "-" + session.Players[i].CurrBetMoney;
                    }
                    else
                        message = String.Format("{0}\t{1}", message, session.Players[i].CurrBetMoney);
                    Console.WriteLine(message);
                }
                Console.WriteLine("------------------------------------------------------------------------------------");
            }
            
        }
    }
}
