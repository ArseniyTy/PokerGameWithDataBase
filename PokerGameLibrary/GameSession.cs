using System;
using System.Collections.Generic;
using PokerGameLibrary.Cards.Enums;
using PokerGameLibrary.Cards;
using PokerGameLibrary.GamePlayer.Enums;
using PokerGameLibrary.GamePlayer;

// TODO:
// В перспективе: сделать возможность играть с теми же(у кого меньше мин ставки, тот вылетает
// , update gamesession,)

namespace PokerGameLibrary
{
    /// <summary>
    /// A base abstract class for all the sessions. Has some base logic.
    /// </summary>
    public abstract class GameSession
    {

        private DateTime _beginningTime;
        private void TimeUpdate()
        {
            if (!Finished) //if not finished then updating _time
            {
                DateTime currTime = DateTime.UtcNow;
                _time = (int)currTime.Subtract(_beginningTime).TotalSeconds;
            }
        }
        private int _time=0;
        /// <summary>
        /// Returns time that has passed since the beginning of the session.
        /// </summary>
        public int Time
        {
            get
            {
                TimeUpdate();
                return _time;
            }
        }



        /// <summary>
        /// Equals true when the game is finished
        /// </summary>
        public bool Finished { get; private set; }
        /// <summary>
        /// Storages the card deck that is left. 
        /// </summary>
        protected List<Card> _deck = new List<Card>();
        /// <summary>
        /// Storages common cards of all the players.
        /// </summary>
        public List<Card> tableCards = new List<Card>();



        /// <summary>
        /// Storages list of the players, who can still make some decisions.
        /// </summary>
        public List<Player> inGamePlayers = new List<Player>();
        /// <summary>
        /// Updates the list of the players, who can still make some decisions.
        /// </summary>
        private void InGamePlayersUpdater()
        {
            //ищем, кто ходит первый
            int startPlayerIndex = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Role == PlayerRole.BigBlind)
                {
                    if (i != Players.Count - 1)
                        startPlayerIndex = i + 1;
                    break;
                }
            }

            inGamePlayers = new List<Player>();
            for (int i = startPlayerIndex; i < Players.Count; i++)
            {
                if (Players[i].Status != PlayerStatus.Fold && Players[i].Status != PlayerStatus.AllIn)
                    inGamePlayers.Add(Players[i]);
            }
            for (int i = 0; i < startPlayerIndex; i++)
            {
                if (Players[i].Status != PlayerStatus.Fold && Players[i].Status != PlayerStatus.AllIn)
                    inGamePlayers.Add(Players[i]);
            }
        }



        private List<Player> _players = new List<Player>();
        /// <summary>
        /// Storages list of the all players.
        /// </summary>
        /// <exception cref="Exception">Count of the players should be [3;9].</exception>
        public List<Player> Players
        {
            get { return _players; }
            set
            {
                if (value == null)
                    throw new Exception("Count of players cannot be equal to null!");
                if (value.Count < 3)
                    throw new Exception("Count of players must be not less than 3!");
                if (value.Count > 9)
                    throw new Exception("Count of players must be less than 9!");
                else
                {
                    foreach (var player in value)
                    {
                        if (player.Money < 2 * MinBet)
                            throw new Exception("Amount of money must be at least 2*MinimumBet!");
                    }
                    _players = value;
                }
            }
        }



        /// <summary>
        /// Storages the amount of all bets, called Bank
        /// </summary>
        public int Bank { get; private set; }
        //лист банков игроков, за которые они борются
        //public List<int> PlayerBanks { get; private set; }
        //обновляет банк игроков и общий банк после всех торгов
        //private void PlayerBanksUpdate()
        //{
        //    for (int i = 0; i < Players.Count; i++)
        //    {
        //        switch (Players[i].Status)
        //        {
        //            case PlayerStatus.AllIn:
        //            case PlayerStatus.InAuction:
        //                {
        //                    Bank += Players[i].CurrBetMoney;
        //                    PlayerBanks[i] = Bank;
        //                    break;
        //                }
        //        }
        //    }
        //}



        private int _minBet;
        /// <summary>
        /// Storages the minimum bet that is allowed.
        /// </summary>
        /// <exception cref="Exception">Throws when the value is incorrect.</exception>
        public int MinBet
        {
            get { return _minBet; }
            set
            {
                if (value <= 0)
                    throw new Exception("Minimum bet must be more then 0!");
                if (value % 10 != 0)
                    throw new Exception("Minimum bet must divide on 10!");
                else
                    _minBet = value;
            }
        }




        /// <summary>
        /// Simple constructor. Has some important initialization logic inside.
        /// </summary>
        /// <param name="minBet">The minimum bet in the game.</param>
        /// <param name="moneyOfThePlayers">Integer array, which representes the amount of money they have</param>
        public GameSession(int minBet, params int[] moneyOfThePlayers)
        {
            //инициализация
            Bank = 0;
            Finished = true;
            MinBet = minBet;
            if (moneyOfThePlayers != null)
            {
                foreach (var sum in moneyOfThePlayers)
                {
                    Players.Add(new Player(sum));
                    //PlayerBanks.Add(0);
                }
            }
            //Устанавливает минимальную ставку каждому игроку
            foreach (var p in Players)
            {
                p.MinBet = MinBet;
            }
            GameSessionUpdate();
        }

        private void GameSessionUpdate()
        {
            if (!Finished) //нельзя обновить незаконченную сессию
                return;


            //инициализация
            _beginningTime = DateTime.UtcNow;
            MinBet = MinBet; //чтобы сработала внутри логика
            Players = Players;


            //Generation of the decks
            _deck = new List<Card>();
            tableCards = new List<Card>();
            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _deck.Add(new Card((CardSuit)j, (CardValue)i));
                }
            }

            //присваивает роли + 
            //делает blind-ы
            Random random = new Random();
            int randInd = random.Next(0, Players.Count);
            Players[randInd++].Role = PlayerRole.Dealer;

            if (randInd == Players.Count)
                randInd = 0;
            Players[randInd].Role = PlayerRole.SmallBlind;
            Players[randInd++].Bet(MinBet);

            if (randInd == Players.Count)
                randInd = 0;
            Players[randInd].Role = PlayerRole.BigBlind;
            Players[randInd++].Bet(MinBet * 2);



            //раздаёт 2 карты каждому +
            //удаляет розданные карты из колоды
            foreach (var player in Players)
            {
                randInd = random.Next(0, _deck.Count);
                player.CardList.Add(_deck[randInd]);
                _deck.RemoveAt(randInd);

                randInd = random.Next(0, _deck.Count);
                player.CardList.Add(_deck[randInd]);
                _deck.RemoveAt(randInd);
            }

            //т.к. пока что в игре все игроки
            InGamePlayersUpdater();

            Finished = false;
        }





        /// <summary>
        /// Implements some updates after the auction.
        /// </summary>
        private void RoundAfterUpdate()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Status == PlayerStatus.InAuction || Players[i].Status == PlayerStatus.Check)
                {
                    Players[i].Status = PlayerStatus.Waiting;
                }
                Bank += Players[i].CurrBetMoney;
                Players[i].CurrBetMoney = 0;
            }
            Player.MoneyToCall = 0;
        }
        /// <summary>
        /// The main method. Plays one round.
        /// </summary>
        /// <returns>Returns false when the game is finished</returns>
        public bool PlayRound()
        {
            if (Finished)
                return false;

            switch (RoundAction())
            {
                case 1://итог
                    {
                        //PlayerBanksUpdate();
                        RoundAfterUpdate();
                        FinalResult();
                        TimeUpdate();
                        Finished = true;
                        return false;
                    }
                case 0://разадача карт
                    {
                        //PlayerBanksUpdate();
                        RoundAfterUpdate();
                        ShowNewCards();
                        break;
                    }
                case -1://торги
                    {
                        Auction();
                        break;
                    }
            }
            return true;
        }


        /// <summary>
        /// Defines what to do in the game next: show new card / final result / auction.
        /// </summary>
        /// <returns>Returns one of: 1(final) / 0(show cards) / -1(auction)</returns>
        private int RoundAction()
        {
            int max = 0;
            for (int j = 0; j < Players.Count; j++)
            {
                if (Players[j].CurrBetMoney > max)
                    max = Players[j].CurrBetMoney;
            }
            Player.MoneyToCall = max;

            int countOfUnable = 0;
            int countOfСheck = 0;
            foreach (var player in Players)
            {
                switch (player.Status)
                {
                    case PlayerStatus.Waiting:
                        {
                            return -1;
                        }
                    case PlayerStatus.Check:
                        {
                            countOfСheck++;
                            break;
                        }
                    case PlayerStatus.Fold:
                        {
                            countOfUnable++;
                            break;
                        }
                    case PlayerStatus.AllIn:
                        {
                            if (player.CurrBetMoney == 0)
                                countOfUnable++;
                            break;
                        }
                }
            }
            //если остался только один в игре(или никого), то итог
            if (countOfUnable >= Players.Count - 1)
            {
                if (Players[0].CardList.Count == 7)
                    return 1;
                else return 0;//чтоб дораздали карт
            }
            if (countOfСheck > 0)
            {
                //если все из оставшихся check, то раздача
                if (countOfСheck == Players.Count - countOfUnable)
                {
                    //если это последний раунд, то итог
                    if (Players[0].CardList.Count == 7)
                        return 1;
                    return 0;
                }
                //значит есть те, кто уже поставил, а кто-то всё ещё check=>торги
                return -1;
            }


            int i = 0;
            while (i < Players.Count)
            {
                if (Players[i].Status == PlayerStatus.InAuction || Players[i].Status == PlayerStatus.AllIn)
                {
                    if (Players[i].CurrBetMoney < Player.MoneyToCall)
                    {
                        if (Players[i].Status != PlayerStatus.AllIn)
                            return -1;
                    }

                }
                i++;
            }
            //если все карты розданы, то итог
            if (Players[0].CardList.Count == 7)
                return 1;
            return 0;
        }




        /// <summary>
        /// Defines logic that is implemented at the end of the game.
        /// </summary>
        protected void FinalResult()
        {
            //Должно быть:
            //проверяет всех активных игроков(их карты)
            //и по очереди отдаёт игрокам ИХ банки
            //      если общий банк = 0, то всё

            //костыль(чтобы потом просто отсортировать игроков): выставляем текщую ставку все возможные выигрышные деньги игрока
            //for (int i = 0; i < Players.Count; i++)
            //{
            //    Players[i].CurrBetMoney = PlayerBanks[i];
            //}
            //while(Bank>0) { ... }




            //У нас:
            //  все скидываются в общак, и похер сколько ты поставил, если выиграл, то забираешь всё

            var sortedPlayers = new List<Player>();
            sortedPlayers.AddRange(Players);
            sortedPlayers.Sort(); //сортирует на основе комбинаций(по не возрастанию)
            //считаем сколько чуваков, у которых такие же комбинации
            int num = 1;
            while (num < sortedPlayers.Count && sortedPlayers[num] == sortedPlayers[0])
                num++;
            //делим среди них банк
            int k = 0;
            while (k < num)
            {
                sortedPlayers[k].Money += Bank / num;
                sortedPlayers[k].CurrBetMoney = Bank / num; //чтобы просто из интерфейса(ex консоль) увидеть, кто выиграл
                k++;
            }
            Bank = 0;
        }
        /// <summary>
        /// Defines logic that is implemented when new cards need to be shown.
        /// </summary>
        protected void ShowNewCards()
        {
            InGamePlayersUpdater();


            Random random = new Random();

            int randCard1 = random.Next(0, _deck.Count);
            tableCards.Add(_deck[randCard1]);
            foreach (var player in Players)
            {
                player.CardList.Add(_deck[randCard1] as Card);
            }
            _deck.RemoveAt(randCard1);

            if (Players[0].CardList.Count == 3) //т.е. 2 свои и + одна сгенерированная
            {
                int randCard2 = random.Next(0, _deck.Count);
                tableCards.Add(_deck[randCard2]);
                _deck.RemoveAt(randCard2);
                int randCard3 = random.Next(0, _deck.Count);
                tableCards.Add(_deck[randCard3]);
                _deck.RemoveAt(randCard3);

                foreach (var player in Players)
                {
                    player.CardList.Add(tableCards[tableCards.Count - 2] as Card);
                    player.CardList.Add(tableCards[tableCards.Count - 1] as Card);
                }
            }

            if (tableCards.Count > 5)
                throw new Exception("Player have more than 7 cards!");

        }
        /// <summary>
        /// Defines logic implemented during the auction.
        /// </summary>
        protected abstract void Auction();
    }
}
