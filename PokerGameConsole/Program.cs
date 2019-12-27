using System;
using DatabaseService;
using PokerGameLibrary.Bots;
using PokerGameLibrary.Bots.BotLevels;
using PokerGameLibrary.GamePlayer;

//arsen
//asdfasdf

namespace PokerGameConsole
{
    class Program
    {
        static void Main()
        {
            Player myPlayer = null;

            while (true)
            {
                while (RegistrationUI(ref myPlayer)) { }
                while (GameUI(ref myPlayer)) { }
            }
            
        }
        public static bool RegistrationUI(ref Player myPlayer)
        {
            var registrationInstructions = "1)Press L to log in\n2)Press S to sign in\n3)Press ESC to quit the game";

            Console.Clear();
            Console.WriteLine(registrationInstructions);
            var key = Console.ReadKey();
            Console.Clear();
            switch(key.Key)
            {
                case ConsoleKey.L:
                    {
                        Console.WriteLine("Enter your name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter amount of money:");
                        int money = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter password:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Repeat your password:");
                        string passwordRepeat = Console.ReadLine();

                        if (password != passwordRepeat)
                        {
                            Console.WriteLine("Your repeted password doesn't match password");
                            Console.ReadKey();
                        }
                        else
                        {
                            Registration.SignUp(name, money, password);
                            Console.WriteLine("Your registration is completed!");
                            Console.WriteLine("Sign in now");
                            Console.ReadKey();
                        }
                        break;
                    }
                case ConsoleKey.S:
                    {
                        Console.WriteLine("Enter your name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string password = Console.ReadLine();

                        int money;
                        if (Registration.SignIn(name, password, out money))
                        {
                            myPlayer = new Player(money);
                            myPlayer.Name = name;
                            Console.WriteLine("Your signed in successfully!");
                            Console.ReadKey();
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("There is no such a user!");
                            Console.ReadKey();
                        }
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
            return true;
        }
        public static bool GameUI(ref Player myPlayer)
        {
            var gameEnteringInstructions = "1)Press ESC to quit\n2)Press Backspace to sign out\n3)Press Enter to start the game\n4)Press Y to watch the statistics of the player games\n5)Press S to go to settings";
            IBotLevel botLvl = null;

            Console.Clear();
            Console.WriteLine(gameEnteringInstructions);
            var key = Console.ReadKey();
            Console.Clear();
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    {
                        Environment.Exit(0);
                        break;
                    }
                case ConsoleKey.Backspace:
                    {
                        return false;
                    }
                case ConsoleKey.Y: //game statistics
                    {
                        GameInfoDrawer.PlayerGameStatViewConsole(myPlayer);
                        Console.ReadKey();
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
                        break;
                    }
                case ConsoleKey.Enter:
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
                        session.myPlayer.Name = myPlayer.Name; //даём ему имя
                        myPlayer = session.myPlayer; //теперь в переменной уже ссылка на плеера из сессии
                        GameInfoDrawer.DrawInfo(session);



                        while (session.PlayRound())
                        {
                            if (session.WaitingForMyPlayer)
                            {
                                GameSessionUI(ref myPlayer);
                                session.WaitingForMyPlayer = false;
                            }
                            Console.Clear();
                            GameInfoDrawer.DrawInfo(session);
                        }
                        GameStatistics.PlayerGameStatAdd(session); //запись истории игр в бд
                        GameStatistics.PlayerDatabaseUpdater(myPlayer); //обновляет деньги плееру
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        GameInfoDrawer.DrawInfo(session);
                        Console.ReadKey();
                        break;
                    }
            }
            return true;
        }
        public static void GameSessionUI(ref Player myPlayer)
        {
            var playerDecisionInstruction = "1)Press V to check\n2)Press A to all-in\n3)Press F to fold\n4)Press B to bet and then enter the bet\n5)Press R to raise(at least x2) and then enter the bet\n6)Press C to call";
            Console.WriteLine(playerDecisionInstruction);
            var key = Console.ReadKey();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
