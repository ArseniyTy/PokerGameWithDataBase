using System;
using System.Collections.Generic;
using System.Text;
using PokerGameLibrary.Interfaces;
using PokerGameLibrary.Enums;

namespace PokerGameLibrary.Classes
{

    /// <summary>
    /// Class that describes <c>Player</c> entity.
    /// <para>Implements interfaces <c>IPlayer</c>, <c>IPlayerCards</c>, <c>IPlayerGameActions</c>, <c>IComparable</c>.</para>
    /// </summary>   
    public class Player : IPlayer, IPlayerCards, IPlayerGameActions, IComparable<Player>
    {
        public string Name { get; set; }

        /// <summary>
        /// Storages money the player must call.
        /// </summary>
        /// <exception cref="Exception">Throws when the player has to call a negative amount of money.</exception>
        private static int _moneyToCall = 0;
        public static int MoneyToCall
        {
            get { return _moneyToCall; }
            set
            {
                if (value < 0)
                    throw new Exception("Players cannot call negative amount of money");
                _moneyToCall = value;
            }
        }


        /// <summary>
        /// Storages player money.
        /// </summary>
        /// <exception cref="Exception">Throws when the player has negative amount of money.</exception>
        private int _money;
        public int Money
        {
            get { return _money; }
            set 
            {
                if (value < 0)
                    throw new Exception("The player cannot have negative amount of money");
                _money = value; 
            }
        }

        /// <summary>
        /// Storages player current bet. It equals to null, if player hasn't bet yet.
        /// </summary>
        /// <exception cref="Exception">Throws when the player has negative amount of betted money.</exception>
        private int _currBetMoney = 0;
        public int CurrBetMoney
        {
            get { return _currBetMoney; }
            set
            {
                if (value < 0)
                    throw new Exception("The player cannot have negative amount of money");
                _currBetMoney = value;
            }
        }
        /// <summary>
        /// Storages game status of the player.
        /// </summary>
        /// <value>Gets the enum <c>PlayerStatus</c>.</value>
        public PlayerStatus Status { get; set; }
        /// <summary>
        /// Storages game role of the player(D, SB, BB).
        /// </summary>
        /// <value>Gets the enum <c>PlayerRole</c>.</value>
        public PlayerRole Role { get; set; }




        private List<string> _possibleGameActionsMethods;
        /// <summary>
        /// Updates the list of methods(that are game actions) that we can use in the current situation.  
        /// </summary>
        /// <param name="prevPlayer">Storages the previous active(still in the game) player.</param>
        public void PossibleGameActionsMethodsUpdater(Player prevPlayer)
        {
            _possibleGameActionsMethods.Clear();
            if (Status == PlayerStatus.Check || Status == PlayerStatus.InAuction || Status == PlayerStatus.Waiting)
            {
                _possibleGameActionsMethods.Add("AllIn");
                _possibleGameActionsMethods.Add("Fold");


                switch (prevPlayer.Status)
                {
                    case PlayerStatus.AllIn:
                    case PlayerStatus.InAuction:
                        {
                            _possibleGameActionsMethods.Add("Call");
                            _possibleGameActionsMethods.Add("Raise");
                            break;
                        }
                    case PlayerStatus.Waiting:
                    case PlayerStatus.Check:
                        {
                            _possibleGameActionsMethods.Add("Check");
                            if (Status != PlayerStatus.InAuction)
                                _possibleGameActionsMethods.Add("Bet");
                            break;
                        }
                }
            }
        }



        /// <summary>
        /// Makes a Fold in the game.
        /// </summary>
        /// <exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void Fold()
        {
            if (!_possibleGameActionsMethods.Contains("Fold"))
                throw new Exception("You cannot use this method in current game situation!");
            Status = PlayerStatus.Fold;
        }
        /// <summary>
        /// Makes a Check in the game.
        /// </summary>
        ///<exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void Check()
        {
            if (!_possibleGameActionsMethods.Contains("Check"))
                throw new Exception("You cannot use this method in current game situation!");
            Status = PlayerStatus.Check;
        }
        /// <summary>
        /// Makes a Bet in the game.
        /// </summary>
        /// <exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void Bet(int money)
        {
            if (!_possibleGameActionsMethods.Contains("Bet") && MoneyToCall!=0)
                throw new Exception("You cannot use this method in current game situation!");

            Status = PlayerStatus.InAuction;
            int delta = money - CurrBetMoney;
            if (delta > Money)
            {
                AllIn();
            }
            else
            {
                CurrBetMoney += delta;
                Money -= delta;
            }
        }
        /// <summary>
        /// Makes a Call in the game.
        /// </summary>
        /// <exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void Call()
        {
            if (!_possibleGameActionsMethods.Contains("Call"))
                throw new Exception("You cannot use this method in current game situation!");
            _possibleGameActionsMethods.Add("Bet");//чтобы потом в bet не выдало ошибку
            Bet(MoneyToCall);
            
        }
        /// <summary>
        /// Makes a Raise in the game.
        /// </summary>
        /// <exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void Raise(int money)
        {
            if (!_possibleGameActionsMethods.Contains("Raise"))
                throw new Exception("You cannot use this method in current game situation!");
            _possibleGameActionsMethods.Add("Bet");//чтобы потом в bet не выдало ошибку

            if(money<MoneyToCall*2)
                throw new Exception("You can raise not less than x2!");
            Bet(money);
        }
        /// <summary>
        /// Makes an All-In in the game.
        /// </summary>
        /// <exception cref="Exception">Throws when the player cannot make this action.</exception>
        public void AllIn()
        {
            if (!_possibleGameActionsMethods.Contains("AllIn"))
                throw new Exception("You cannot use this method in current game situation!");
            Status = PlayerStatus.AllIn;
            CurrBetMoney += Money;
            Money = 0;
        }




        /// <summary>
        /// The main full constructor.
        /// </summary>
        /// <param name="card1">First playing card of the player.</param>
        /// <param name="card2">Second playing card of the player.</param>
        /// <param name="money">Money of the player.</param>
        /// <param name="betMoney">Current bet of the player.</param>
        /// <param name="playerStatus">The game status of the player.</param>
        /// <param name="playerRole">The game role of the player</param>
        public Player(List<Card> cardList, int money, int betMoney, PlayerStatus playerStatus, PlayerRole playerRole)
        {
            CardList = cardList;
            Money = money;
            CurrBetMoney = betMoney;
            Status = playerStatus;
            Role = playerRole;
            _possibleGameActionsMethods = new List<string>();
        }
        /// <summary>
        /// The simplified constructor. Good to use before the beginning of the game.
        /// </summary>
        /// <param name="money">Money of the player.</param>
        public Player(int money) : this(null, money, 0, PlayerStatus.Waiting, PlayerRole.None) { }
        /// <summary>
        /// Base constructor
        /// </summary>
        public Player() : this(null, 0, 0, PlayerStatus.Waiting, PlayerRole.None) { }



        /// <summary>
        /// Compares 2 Players (compares the cards of the players, as it can't be 2 different players with the same cards in 1 game).
        /// </summary>
        /// <param name="other">The player to compare with. </param>
        /// <returns>Returns 1 if this players is greater, 0 - if players are equal and -1 otherwise</returns>
        public int AllCardsEqual(IPlayerCards other)
        {
            if (other == null)
                throw new Exception("Comparing object equals to null");

            for (int i = 0; i < CardList.Count; i++)
            {
                if(CardList[i].CompareTo(other.CardList[i])!=0)
                    return -CardList[i].CompareTo(other.CardList[i]);
            }
            return 0;
        }

        public int CompareTo(Player other)
        {
            if (other == null)
                throw new Exception("Comparing object equals to null");
            if (other.CardList.Count < 7)
                return -1;
            if (CardList.Count < 7)
                return 1;

            if (CardCombination() > other.CardCombination())
                return -1;
            else if (CardCombination() == other.CardCombination())
                return 0;
            else
                return 1;
        }



        /// <summary>
        /// Storages all the cards of the player.
        /// </summary>
        private List<Card> _cardList;
        public List<Card> CardList
        {
            get { return _cardList; }
            set 
            {
                if (value == null)
                    _cardList = new List<Card>();
                else
                    _cardList = value;
            }
        }
        /// <summary>
        /// Counts the priority of the card combination, by which we can define the winner. 
        /// </summary>
        /// <returns>The priority of the card combination.</returns>
        public int CardCombination()
        {
            //«Роял Стрит Флеш» – 5 самых старших одномастных карт.
            //«Стрит Флеш» – 5 карт одной масти по порядку.
            //«Каре» – 4 карты одного ранга.
            //«Фулл Хаус» – комбинация, включающая в себя «Пару» и «Тройку» одновременно.
            //«Флеш» – 5 одномастных карт.
            //«Стрит» – 5 собранных по порядку карт любой масти.
            //«Сет» или «Тройка» – 3 карты одного ранга.
            //«Две пары» – 4 карты, среди которых собраны по 2 одинаковых по рангу.
            //«Пара» – это 2 одинаковые карты.
            //«Старшая карта» – это 1 наивысшая по масти карта.

            if (Status == PlayerStatus.Fold)
                return 0;

            int priority = 1000000; //базовый приоритет комбинации(коэффицент, с помощью которого рассчитывается значимость комбинации)
            //using CompareTo() in Card.cs - сортировка по не возрастанию
            var cardsListSorted = new List<Card>();
            cardsListSorted.AddRange(CardList);
            cardsListSorted.Sort();


            //1 group to check

            //количество каждой масти
            var cardSuitsNum = new int[4];
            //количество подряд идущих отличающихся на 1
            int diffOn1InRowNum = 0;
            //количество равных подряд идущих
            int countOfEq = 0;
            //индекс первой карты straigth (для проверки на стрит-флеш)
            int indOfFirstStraight = -1;
            //индекс масти flesh 
            int flushCardSuit = -1;
            cardSuitsNum[(int)cardsListSorted[0].CardSuit]++;//в цикле с нуля начинается
            for (int i = 1; i < cardsListSorted.Count; i++)
            {
                cardSuitsNum[(int)cardsListSorted[i].CardSuit]++;
                if (cardsListSorted[i-1].CardValue - cardsListSorted[i].CardValue == 1 || cardsListSorted[i - 1].CardValue - cardsListSorted[i].CardValue == 0)
                {
                    diffOn1InRowNum++;
                    if (diffOn1InRowNum==1)//если это первая находка пары, то надо их обоих посчитать
                        diffOn1InRowNum++;
                    if (cardsListSorted[i - 1].CardValue - cardsListSorted[i].CardValue == 0)
                        countOfEq++;
                }
                else
                {
                    if (diffOn1InRowNum-countOfEq >= 5)
                    {
                        indOfFirstStraight = i - diffOn1InRowNum;
                    }
                    else
                        diffOn1InRowNum = 0;
                }
            }
            //если все карты на 1 отличаются и верхняя не сработала
            if (diffOn1InRowNum - countOfEq >= 5 && indOfFirstStraight==-1)
            {
                indOfFirstStraight = cardsListSorted.Count - diffOn1InRowNum;
            }
            for (int i = 0; i < 4; i++)
            {
                if (cardSuitsNum[i] >= 5)
                    flushCardSuit = i;
            }


            if (indOfFirstStraight != -1 && flushCardSuit != -1)
            {
                int count = 0;
                for (int i = indOfFirstStraight; i < indOfFirstStraight + diffOn1InRowNum; i++)
                {
                    if (cardsListSorted[i].CardSuit != (CardSuit)flushCardSuit)
                        count++;
                }
                if(diffOn1InRowNum-count<5)
                    indOfFirstStraight = -1; //т.е отменяю стрит, т.к. масти флеша и стрита не совпадают, а флеш важнее стрита


                //если всё же у нас стрит-флеш
                if (indOfFirstStraight != -1)
                {
                    if (cardsListSorted[indOfFirstStraight].CardValue == CardValue.Ace)
                        return 10 * priority; //royal flush
                    else
                        return 9 * priority + (int)cardsListSorted[indOfFirstStraight].CardValue; //straight flush
                }

            }







            //2 group to check

            var cardsCount = new List<int> { 1 };
            for (int i = 1; i < cardsListSorted.Count; i++)
            {
                if (cardsListSorted[i].CardValue == cardsListSorted[i - 1].CardValue)
                    cardsCount[cardsCount.Count - 1]++;
                else
                    cardsCount.Add(1);
            }



            var cardsCountSorted = cardsCount;
            cardsCountSorted.Sort();
            cardsCountSorted.Reverse();
            //находим индекс в cardsCount карты с наибольшем повторением
            int ind = 0;
            for (int i = 0; i < cardsCount.Count; i++)
            {
                if (cardsCount[i] == cardsCountSorted[0])
                    break;
                ind += cardsCount[i];
            }


            if (cardsCountSorted[0] == 4)
            {
                int sum = (priority / 100) * (int)cardsListSorted[ind].CardValue;
                if (ind == 0)
                    sum += (priority / 1000) * (int)cardsListSorted[4].CardValue;
                else
                    sum += (priority / 1000) * (int)cardsListSorted[0].CardValue;

                return 8 * priority + sum; //four of a kind (каре)
            }
            else if (cardsCountSorted[0] == 3 && cardsCountSorted[1] >= 2)
            {
                    int sum = (priority / 100) * (int)cardsListSorted[ind].CardValue;
                    for (int i = 1; i < cardsListSorted.Count; i++)
                    {
                        if (i - ind > 2 && cardsListSorted[i].CardValue == cardsListSorted[i - 1].CardValue)
                        {
                            sum += (priority / 1000) * (int)cardsListSorted[i].CardValue;
                            break;
                        }
                    }

                    return 7 * priority + sum; //full house
            }

            //вставочка с ещё более раннего этапа
            if (flushCardSuit != -1)
            {
                int sum = 0;
                int n = (priority / 100);
                for (int i = 0; n != 0; i++)
                {
                    if ((int)cardsListSorted[i].CardSuit == flushCardSuit)
                    {
                        sum += n * (int)cardsListSorted[i].CardValue; //при флеше сравниваются сначала старшие карты, а потом по порядку
                        n /= 10;
                    }
                }
                return 6 * priority + sum; //flush
            }
            if (indOfFirstStraight != -1)
                return 5 * priority + (int)cardsListSorted[indOfFirstStraight].CardValue; //straight



            if (cardsCountSorted[0] == 3)//но тут уже проверено, что не фул-хаус
            {
                int sum = (priority / 100) * (int)cardsListSorted[ind].CardValue;

                if (ind == 0)
                {
                    sum += (priority / 1000) * (int)cardsListSorted[3].CardValue;
                    sum += (priority / 1000) * (int)cardsListSorted[4].CardValue;
                }
                else
                {
                    sum += (priority / 1000) * (int)cardsListSorted[0].CardValue;
                    if (ind == 1)
                        sum += (priority / 1000) * (int)cardsListSorted[4].CardValue;
                    else
                        sum += (priority / 1000) * (int)cardsListSorted[1].CardValue;
                }

                return 4 * priority + sum; //three of a kind (сет)
            }
            else if (cardsCountSorted[0] == 2)
            {
                if (cardsCountSorted[1] == 2)
                {
                    int sum = 0;
                    bool noHighCard = true;
                    int n = 2;
                    for (int i = 1; n != 0; i++)
                    {
                        if (cardsListSorted[i].CardValue == cardsListSorted[i - 1].CardValue)
                        {
                            sum += (priority / 100) * (int)cardsListSorted[i].CardValue;
                            n--;
                        }
                        else if (noHighCard)//учёт старшей карты
                            sum += (priority / 1000) * (int)cardsListSorted[i].CardValue;
                    }

                    return 3 * priority + sum; //two pair
                }
                else
                {
                    int sum = 0, highCardCount = 3;
                    int multip = (priority / 100);
                    for (int i = 1; i < cardsListSorted.Count; i++)
                    {
                        if (cardsListSorted[i].CardValue == cardsListSorted[i - 1].CardValue)
                        {
                            sum += (priority / 100) * (int)cardsListSorted[i].CardValue;
                        }
                        else if (highCardCount-- > 0)//учёт старшей карты
                        {
                            sum += multip * (int)cardsListSorted[i].CardValue;
                            multip /= 10;
                        }
                        else
                            break;
                    }

                    return 2 * priority + sum; //pair
                }
            }
            else
            {
                int sum = 0, multip = (priority / 100);
                for (int i = 0; i < 5; i++)
                {
                    sum += multip * (int)cardsListSorted[i].CardValue;
                    multip /= 10;
                }

                return priority + sum; //high card
            }
        }
    }
}
