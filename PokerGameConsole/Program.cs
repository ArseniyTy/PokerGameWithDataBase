using System;
using PokerGameLibrary.Classes.Bots;
using PokerGameLibrary.Classes;
using PokerGameLibrary.Interfaces;


namespace PokerGameConsole
{
    class Program
    {
        static void Main()
        {
            var beginInstructions = "1)Press ESC to quit\n2)Press Enter to start the game\n3)Press Y to create new player\n4)Press S to go to settings";
            var playerDecisionInstruction = "1)Press V to check\n2)Press A to all-in\n3)Press F to fold\n4)Press B to bet and then enter the bet\n5)Press R to raise(at least x2) and then enter the bet\n6)Press C to call";
            IBotLevel botLvl=null;
            Player myPlayer = null;
            var key = new ConsoleKeyInfo();
            while (true)
            {
                Console.WriteLine(beginInstructions);
                key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    case ConsoleKey.Y:
                        {
                            Console.WriteLine("Write down the amount of money you have:");
                            int money = int.Parse(Console.ReadLine());
                            myPlayer = new Player(money);
                            Console.Clear();
                            break;
                        }
                    case ConsoleKey.S:
                        {
                            Console.WriteLine("Write down the level of the bot(1 letter): S - silly, M - medium");
                            key = Console.ReadKey();
                            Console.Clear();
                            switch (key.Key)
                            {
                                case ConsoleKey.S:
                                    {
                                        botLvl = new SillyBot();
                                        break;
                                    }
                                case ConsoleKey.M:
                                    {
                                        botLvl = new MediumBot();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("You haven't set the level!");
                                        Console.ReadKey();
                                        break;
                                    }
                            }
                            Console.Clear();
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            if (myPlayer == null)
                                Console.WriteLine("Create your player first!");
                            else
                            {
                                Console.WriteLine("How much people you want to play with(>1 & <9)?");
                                int peopCount = int.Parse(Console.ReadLine());
                                Console.WriteLine("What is the minimum bet in your game(divided on 10)?");
                                int minBet = int.Parse(Console.ReadLine());
                                Console.Clear();

                                var plMoney = new int[peopCount + 1];
                                plMoney[0] = myPlayer.Money;
                                Random rnd = new Random();
                                for (int i = 1; i < peopCount + 1; i++)
                                {
                                    plMoney[i] = rnd.Next(30 * minBet, 250 * minBet);
                                }

                                GameBotSession session;
                                try
                                {
                                    session = new GameBotSession(minBet, plMoney);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }

                                session.SetBotLevel(botLvl);
                                myPlayer = session.myPlayer;
                                GameInfoDrawer.DrawInfo(session);

                                //TODO
                                //Проверка выигрышных комбинаций через тесты
                                

                                while (session.PlayRound())
                                {
                                    if (session.WaitingForMyPlayer)
                                    {
                                        Console.WriteLine(playerDecisionInstruction);
                                        key = Console.ReadKey();
                                        Console.Clear();
                                        try
                                        {
                                            switch (key.Key)
                                            {
                                                case ConsoleKey.V:
                                                    {
                                                        myPlayer.Check();
                                                        break;
                                                    }
                                                case ConsoleKey.A:
                                                    {
                                                        myPlayer.AllIn();
                                                        break;
                                                    }
                                                case ConsoleKey.F://fix(даже если фолд, то ты выигрываешь)
                                                    {
                                                        myPlayer.Fold();
                                                        break;
                                                    }
                                                case ConsoleKey.B:
                                                    {
                                                        int bet = int.Parse(Console.ReadLine());
                                                        myPlayer.Bet(bet);
                                                        break;
                                                    }
                                                case ConsoleKey.R:
                                                    {
                                                        int raise = int.Parse(Console.ReadLine());
                                                        myPlayer.Raise(raise);
                                                        break;
                                                    }
                                                case ConsoleKey.C:
                                                    {
                                                        myPlayer.Call();
                                                        break;
                                                    }
                                            }
                                            session.WaitingForMyPlayer = false;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            Console.ReadKey();
                                        }
                                    }
                                    Console.Clear();
                                    GameInfoDrawer.DrawInfo(session);
                                }
                                System.Threading.Thread.Sleep(2000);
                                Console.Clear();
                                GameInfoDrawer.DrawInfo(session);
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
