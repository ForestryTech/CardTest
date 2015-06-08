using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
    class Program
    {
        static void Main(string[] args) {
            List<Card> cards = new List<Card>() {
                new Card(Suits.Clubs, Values.Eight),
                new Card(Suits.Clubs, Values.Five),
                new Card(Suits.Clubs, Values.Nine),
                new Card(Suits.Diamonds, Values.Queen),
                new Card(Suits.Hearts, Values.Eight),
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Clubs, Values.Two)
            };

            List<Card> straight = new List<Card>() {
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Diamonds, Values.Four),
                new Card(Suits.Hearts, Values.Ten),
                new Card(Suits.Diamonds, Values.Six),
                new Card(Suits.Hearts, Values.Three),
                new Card(Suits.Clubs, Values.Five),
                new Card(Suits.Hearts, Values.Ace)
            };

            List<Card> aceLow = new List<Card>() {
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Diamonds, Values.Four),
                new Card(Suits.Hearts, Values.Seven),
                new Card(Suits.Diamonds, Values.Three),
                new Card(Suits.Hearts, Values.Ace),
                new Card(Suits.Clubs, Values.Five),
                new Card(Suits.Hearts, Values.Ace)
            };

            List<Card> FourOfAKind = new List<Card>() {
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Diamonds, Values.Two),
                new Card(Suits.Hearts, Values.Six),
                new Card(Suits.Diamonds, Values.Three),
                new Card(Suits.Hearts, Values.Two),
                new Card(Suits.Clubs, Values.Three),
                new Card(Suits.Spades, Values.Two)
            };

            List<Card> FullHouse = new List<Card>() {
                new Card(Suits.Clubs, Values.Two),
                new Card(Suits.Diamonds, Values.Two),
                new Card(Suits.Hearts, Values.Six),
                new Card(Suits.Diamonds, Values.Three),
                new Card(Suits.Hearts, Values.Three),
                new Card(Suits.Clubs, Values.Five),
                new Card(Suits.Spades, Values.Two)
            };


            Console.WriteLine("Testing for four of a kind: ***\n");
            if (isFourOfAKind(FourOfAKind)) {
                sortFourOfAKind(FourOfAKind);
            }

            Console.ReadLine();
        }

        static void sortFourOfAKind(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            List<Card> BestHand = new List<Card>();
            List<Card> OtherCards = new List<Card>();
            Values fourOfAKindValue = Values.Ace;
            count = getCardDictionary(cards);
            foreach (var item in count) {
                if (item.Value == 4)
                    fourOfAKindValue = (Values)item.Key;
            }
            foreach (Card card in cards) {
                if (card.Value == fourOfAKindValue)
                    BestHand.Add(card);
                else
                    OtherCards.Add(card);
            }
            Console.Write("\nBest hand: \n");
            foreach (Card card in BestHand)
	        {
		        Console.Write("{0} ", card.ToString());
	        }
            Console.Write("\nOther Hand: \n");
            foreach (Card card in OtherCards)
	        {
		        Console.Write("{0} ", card.ToString());
	        }

        }
        static bool isFourOfAKind(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            count = getCardDictionary(cards);
            foreach (var item in count) {
                if (item.Value == 4)
                    return true;
            }
            return false;
        
        }

        static bool isThreeOfAKind(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();

            count = getCardDictionary(cards);
            foreach (var item in count) {
                if (item.Value == 3)
                    return true;
            }
            return false;
        }

        static bool isPair(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            count = getCardDictionary(cards);
            foreach (var item in count) {
                if (item.Value == 2)
                    return true;
            }
            return false;
        }

        static bool isTwoPair(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            bool firstPair = false;
            bool secondPair = false;
            count = getCardDictionary(cards);
            foreach (var item in count) {
                if ((item.Value == 2) && !firstPair)
                    firstPair = true;
                if (item.Value == 2 && firstPair)
                    secondPair = true;
            }

            if (firstPair && secondPair)
                return true;
            else
                return false;
        }

        static Dictionary<Values, int> getCardDictionary(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            foreach (Card card in cards) {
                if (count.ContainsKey(card.Value)) {
                    count[card.Value]++;
                } else {
                    count.Add(card.Value, 1);
                }
            }
            return count;
        }

        static bool isFullHouse(List<Card> cards) {
            if (isPair(cards) && isThreeOfAKind(cards))
                return true;
            else
                return false;
            /*Dictionary<Values, int> count = new Dictionary<Values, int>();
            bool threeOfAKind = false;
            bool pair = false;

            foreach (Card card in cards) {
                if (count.ContainsKey(card.Value)) {
                    count[card.Value]++;
                } else {
                    count.Add(card.Value, 1);
                }
            }
            foreach (var item in count) {
                if (item.Value == 3)
                    threeOfAKind = true;
                if (item.Value == 2)
                    pair = true;
            }
            if (threeOfAKind && pair)
                return true;
            else
                return false;*/
        }

        static void DisplayCards(List<Card> cards) {
            foreach (Card card in cards) {

                Console.Write(card.ToString() + " ");

            }
            Console.Write("\n");
        }

        static bool isStraight(List<Card> cards) {
            cards.Sort(new CardComparerValue());
            int straightCount = 0;
            int startValue = 0;
            int straightValue = (int)cards[0].Value;
            int straigtValueToCheck;

            // Check for straight with Aces high
            for (int i = 1; i < cards.Count(); i++) {
                straigtValueToCheck = (int)cards[i].Value;
                
                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4) {
                        Console.Write("First card in straight: {0}\n", cards[startValue].ToString());
                      
                        List<Card> sh = new List<Card>();
                        List<Card> oc = new List<Card>();
                        for (int x = 0; x < cards.Count; x++) {
                            if (x == startValue || x == (startValue + 1) || x == (startValue + 2) || x == (startValue + 3) || x == (startValue + 4))
                                sh.Add(cards[x]);
                            else
                                oc.Add(cards[x]);
                            
                        }
                        Console.Write("\n*********************************************\n");
                        foreach (Card c in sh) {
                            Console.Write("====>{0}", c.ToString());
                        }
                        Console.Write("\n*********************************************\n");
                        Console.Write("******** OTHER CARDS *************************\n");
                        foreach (Card c in oc) {
                            Console.Write("---> {0}", c.ToString());
                        }
                        Console.Write("\n");
                        return true;
                        
                    }
                } else {
                    straightValue = straigtValueToCheck;
                    startValue = i;
                    straightCount = 0;
                }
            }
            // Check for straight with Aces low
            cards.Sort(new CardCompareAcesLow());
            straightCount = 0;
            startValue = 0;
            straightValue = ((int)cards[0].Value == 14) ? 1 : (int)cards[0].Value;
            for (int i = 1; i < cards.Count(); i++) {
                straigtValueToCheck = ((int)cards[i].Value == 14) ? 1 : (int)cards[i].Value;
                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4) {
                        Console.Write("First card in straight: {0}\n", cards[startValue].ToString());
                        List<Card> sh = new List<Card>();
                        List<Card> oc = new List<Card>();
                        for (int x = 0; x < cards.Count; x++) {
                            if (x == startValue || x == (startValue + 1) || x == (startValue + 2) || x == (startValue + 3) || x == (startValue + 4))
                                sh.Add(cards[x]);
                            else
                                oc.Add(cards[x]);
                        }
                        Console.Write("\n*********************************************\n");
                        foreach (Card c in sh) {
                            Console.Write("====>{0}", c.ToString());
                        }
                        Console.Write("\n*********************************************\n");
                        Console.Write("******** OTHER CARDS *************************\n");
                        foreach (Card c in oc) {
                            Console.Write("---> {0}", c.ToString());
                        }
                        Console.Write("\n");
                        return true;
                    }
                } else {
                    straightValue = straigtValueToCheck;
                    startValue = i;
                    straightCount = 0;
                }
            }
            return false;

        }


        class Card
        {
            public Suits Suit { get; set; }
            public Values Value { get; set; }

            public string Name {
                get {
                    string cardValue;
                    string cardSuit = "";
                    switch ((int)Value) {
                        case 11:
                            cardValue = "J";
                            break;
                        case 12:
                            cardValue = "Q";
                            break;
                        case 13:
                            cardValue = "K";
                            break;
                        case 14:
                            cardValue = "A";
                            break;
                        default:
                            cardValue = ((int)Value).ToString();
                            break;
                    }

                    switch ((int)Suit) {
                        case 0:
                            cardSuit = "\u2660";
                            break;
                        case 1:
                            cardSuit = "\u2665";
                            break;
                        case 2:
                            cardSuit = "\u2663";
                            break;
                        case 3:
                            cardSuit = "\u2666";
                            break;
                    }

                    return cardValue + cardSuit;
                }
            }

            public override string ToString() {
                return Name;
            }

            public Card(int suit, int value) {
                Suit = (Suits)suit;
                Value = (Values)value;
            }

            public Card(Suits suit, Values value) {
                this.Suit = suit;
                this.Value = value;
            }

            public Card() { }
        }

        class Player
        {
            internal protected List<Card> cards;
            private string playerName;
            private double cash;
            public Hand PlayerHand { get; set; }

            public double Bet { get; set; }
            public bool Playing { get; private set; }

            public void GetCard(Card card) {
                cards.Add(card);
            }

            public string ShowCards() {
                string currentHand = playerName + " hand: ";
                for (int i = 0; i < cards.Count(); i++) {
                    if (i == (cards.Count() - 1))
                        currentHand += cards[i].Name;
                    else
                        currentHand += cards[i].Name + ", ";
                }
                return currentHand;
            }

            public Player(string name) {
                cards = new List<Card>();
                playerName = name;
            }

            public Player(List<Card> cards) {
                this.cards = cards;
            }
        }

        class Deck
        {
            private List<Card> cards;
            private Random random = new Random();

            public Deck() {
                cards = new List<Card>();
                for (int suit = 0; suit <= 3; suit++) {
                    for (int value = 1; value <= 13; value++) {
                        cards.Add(new Card(suit, value));
                    }
                }
            }

            public Deck(List<Card> initialCards) {
                cards = new List<Card>(initialCards);
            }

            public int Count { get { return cards.Count; } }

            public void Add(Card cardToAdd) {
                cards.Add(cardToAdd);
            }

            public Card Deal(int index) {
                Card cardToDeal = cards[index];
                cards.RemoveAt(index);
                return cardToDeal;
            }

            public void Shuffle() {
                List<Card> newDeck = new List<Card>();
                while (cards.Count > 0) {
                    int cardToMove = random.Next(cards.Count);
                    newDeck.Add(cards[cardToMove]);
                    cards.RemoveAt(cardToMove);
                }
                cards = newDeck;
            }

        }

        class Hand
        {
            public HandType Type {
                get {
                    List<Card> allCards = new List<Card>();
                    foreach (Card card in player.cards) {
                        allCards.Add(card);
                    }
                    foreach (Card card in game.CommunityCards) {
                        allCards.Add(card);
                    }


                    return HandType.Flush;
                }
            }

            public Card PrimaryHigh {
                get {
                    Card tempCard;
                    player.cards.Sort(new CardComparerValue());
                    tempCard = player.cards[0];
                    return tempCard;
                }
            }

            public Card SecondaryHigh {
                get {
                    Card tempCard;
                    player.cards.Sort(new CardComparerValue());
                    tempCard = player.cards[1];
                    return tempCard;
                }
            }

            private Player player;
            private Game game;

            public Hand(Player player) {
                this.player = player;
            }
            /*
            private bool isFlush(List<Card> cards) {
            cards.Sort(new CardComparerSuit());
            int num = cards.Select(s => )
            }

            private bool isStraight(List<Card> cards) {

            }

            private bool isFullHouse(List<Card> cards) {

            }

            private bool isFourOfAKind(List<Card> cards) {

            }

            private bool isThreeOfAKind(List<Card> cards) {

            }

            private bool isTwoPair(List<Card> cards) {

            }

            private bool isPair(List<Card> cards) {

            } */
        }

        class Game
        {
            internal List<Card> CommunityCards;

            internal List<Player> Players;
            internal Deck GameDeck;

            public Game() {
                CommunityCards = new List<Card>();
                Players = new List<Player>();
                GameDeck = new Deck();
            }
        }

        enum Values
        {
            Ace = 14,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
        }

        enum Suits
        {
            Spades,
            Hearts,
            Clubs,
            Diamonds,
        }

        enum HandType
        {
            StraightFlush = 1,
            FourOfAKind = 2,
            FullHouse = 3,
            Flush = 4,
            Straight = 5,
            ThreeOfAKind = 6,
            TwoPair = 7,
            OnePair = 8,
            HighCard = 9,
        }

        class CardComparerValue : IComparer<Card>
        {
            public int Compare(Card x, Card y) {
                if (x.Value > y.Value)
                    return -1;
                if (x.Value < y.Value)
                    return 1;
                if (x.Suit > y.Suit)
                    return 1;
                if (x.Suit < y.Suit)
                    return -1;
                else
                    return 0;
            }
        }
        class CardCompareAcesLow : IComparer<Card>
        {
            public int Compare(Card x, Card y) {
                if (y.Value == Values.Ace)
                    return -1;
                if (x.Value > y.Value)
                    return -1;
                if (x.Value < y.Value)
                    return 1;
                if (x.Suit > y.Suit)
                    return 1;
                if (x.Suit < y.Suit)
                    return -1;
                else
                    return 0;
            }
        }

        class CardComparerSuit : IComparer<Card>
        {
            public int Compare(Card x, Card y) {
                if (x.Suit > y.Suit)
                    return 1;
                if (x.Suit < y.Suit)
                    return -1;
                else
                    return 0;
            }
        }
        class HandComparer : IComparer<Hand>
        {
            public int Compare(Hand x, Hand y) {
                if (x.Type > y.Type)
                    return 1;
                if (x.Type < y.Type)
                    return -1;
                else {
                    if (x.PrimaryHigh.Value > y.PrimaryHigh.Value)
                        return 1;
                    if (x.PrimaryHigh.Value < y.PrimaryHigh.Value)
                        return -1;
                    else {
                        if (x.SecondaryHigh.Value > y.SecondaryHigh.Value)
                            return 1;
                        if (x.SecondaryHigh.Value < y.SecondaryHigh.Value)
                            return -1;
                        else
                            return 0;
                    }
                }
            }
        }
    }
}
