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

            List<Card> bHand = new List<Card>() {
                new Card(Suits.Clubs, Values.Ace),
                new Card(Suits.Diamonds, Values.Eight),
                new Card(Suits.Diamonds, Values.Jack),
                new Card(Suits.Hearts, Values.Three),
                new Card(Suits.Spades, Values.Five),
                new Card(Suits.Clubs, Values.Seven),
                new Card(Suits.Hearts, Values.Queen)};


            testHands(FourOfAKind, "Four of a Kind");
            Console.WriteLine("\n************************************************************\n");
            testHands(cards, "Cards");
            Console.WriteLine("\n************************************************************\n");
            testHands(straight, "Straight");
            Console.WriteLine("\n************************************************************\n");
            testHands(FullHouse, "FullHouse");
            Console.WriteLine("\n************************************************************\n");
            testHands(aceLow, "aceLow");
            Console.WriteLine("\n************************************************************\n");
            testHands(bHand, "High Card");

            Console.ReadLine();
        }

        static void testHands(List<Card> cards, string name) {
            Console.WriteLine("Testing: {0}", name);
            DisplayCards(cards);
            Console.WriteLine("*** IS Four of a Kind");
            if (isFourOfAKind(cards)) {
                Console.WriteLine("****** FOUND FOUR OF A KIND!");
                sortFourOfAKind(cards);
            } else {
                Console.WriteLine("Not four of a kind");
            }

            Console.WriteLine("\n*** IS Three of a Kind");
            if (isThreeOfAKind(cards)) {
                Console.WriteLine("****** FOUND THREE OF A KIND!");
                sortThreeOfAKind(cards);
            } else {
                Console.WriteLine("Not three of a kind");
            }
            Console.WriteLine("\n*** IS Pair");
            if (isPair(cards)) {
                Console.WriteLine("****** FOUND PAIR!");
                sortPair(cards);
            } else {
                Console.WriteLine("Not a pair");
            }
            Console.WriteLine("\n*** IS two Pair");
            if (isTwoPair(cards)) {
                Console.WriteLine("****** FOUND TWO PAIR!");
                sortTwoPair(cards);
            } else {
                Console.WriteLine("Not two pair");
            }
            Console.WriteLine("\n*** IS Full House");
            if (isFullHouse(cards)) {
                Console.WriteLine("****** FOUND FULL HOUSE!");
                sortFullHouse(cards);
            } else {
                Console.WriteLine("Not full house");
            }
            Console.WriteLine("\n*** IS Flush");
            if (isFlush(cards)) {
                Console.WriteLine("****** FOUND A FLUSH!");
                sortFlush(cards);
            } else {
                Console.WriteLine("Not a flush");
            }

            Console.WriteLine("\n*** IS a straight");
            if (isStraight(cards)) {
                Console.WriteLine("****** FOUND A STRAIGHT!");
                sortStraight(cards);
            } else {
                Console.WriteLine("not a straight");
            }

            Console.WriteLine("\n*** IS HighCard");
            sortHighCard(cards);
        }

        static void sortStraight(List<Card> AllCards) {
            AllCards.Sort(new CardComparerValue());
            List<Card> BestHand = new List<Card>();
            List<Card> OtherCards = new List<Card>();
            int straightCount = 0;
            int startValue = 0;
            int straightValue = (int)AllCards[0].Value;
            int straigtValueToCheck;
            bool cont = true;

            // Check for straight with Aces high
            for (int i = 1; i < AllCards.Count(); i++) {
                straigtValueToCheck = (int)AllCards[i].Value;

                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4) {
                        for (int x = 0; x < AllCards.Count; x++) {
                            if (x == startValue || x == (startValue + 1) || x == (startValue + 2) || x == (startValue + 3) || x == (startValue + 4))
                                BestHand.Add(AllCards[x]);

                        }
                        cont = false;
                        break;
                    }
                } else {
                    straightValue = straigtValueToCheck;
                    startValue = i;
                    straightCount = 0;
                }
            }
            if (cont == false) {
                Console.Write("Best Hand: ");
                DisplayCards(BestHand);
                return;
            }
            // Check for straight with Aces low
            AllCards.Sort(new CardCompareAcesLow());
            straightCount = 0;
            startValue = 0;
            straightValue = ((int)AllCards[0].Value == 14) ? 1 : (int)AllCards[0].Value;
            for (int i = 1; i < AllCards.Count(); i++) {
                straigtValueToCheck = ((int)AllCards[i].Value == 14) ? 1 : (int)AllCards[i].Value;
                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4) {
                        for (int x = 0; x < AllCards.Count; x++) {
                            if (x == startValue || x == (startValue + 1) || x == (startValue + 2) || x == (startValue + 3) || x == (startValue + 4))
                                BestHand.Add(AllCards[x]);
                        }
                        break;
                    }
                } else {
                    straightValue = straigtValueToCheck;
                    startValue = i;
                    straightCount = 0;
                }
            }
            Console.Write("Best Hand: ");
            DisplayCards(BestHand);
        }

        static void sortFlush(List<Card> AllCards) {
            Suits suit = Suits.Clubs;
            List<Card> BestHand = new List<Card>();
            List<Card> OtherCards = new List<Card>();
            int[] cardsPerSuit = new int[4];
            foreach (Card card in AllCards) {
                cardsPerSuit[(int)card.Suit]++;
                if (cardsPerSuit[(int)card.Suit] > 4) {
                    suit = card.Suit;
                    break;
                }
            }
            AllCards.Sort(new CardComparerValue());
            AllCards.Reverse();
            int ctr = 0;
            foreach (Card card in AllCards) {
                if ((card.Suit == suit) && ctr < 5) {
                    BestHand.Add(card);
                    ctr++;
                }
            }

            Console.Write("Best Hand: ");
            DisplayCards(BestHand);

        }

        static void sortFourOfAKind(List<Card> cards) {
            Dictionary<Values, int> count = new Dictionary<Values, int>();
            List<Card> temp = cards.ToList();
            
            List<Card> BestHand = new List<Card>();
            Values fourOfAKindValue = Values.Ace;
            count = getCardDictionary(cards);
            foreach (var item in count) {
                if (item.Value == 4)
                    fourOfAKindValue = (Values)item.Key;
            }


            BestHand = cards.Where(c => c.Value == fourOfAKindValue).ToList();
            temp = cards.Where(c => c.Value != fourOfAKindValue).ToList();
            temp.Sort(new CardComparerValue());
            BestHand.Add(temp[0]);

            Console.Write("Best Hand: ");
            DisplayCards(BestHand);

        }

        static void sortThreeOfAKind(List<Card> cards) {
            List<Card> BestHand = new List<Card>();
            List<Card> temp = cards.ToList();
            Dictionary<Values, int> count = getCardDictionary(cards);
            Values threeOfAKind = Values.Ace;

            foreach (var item in count) {
                if (item.Value == 3)
                    threeOfAKind = item.Key;
            }

            BestHand = cards.Where(c => c.Value == threeOfAKind).ToList();
            temp = cards.Where(c => c.Value != threeOfAKind).ToList();
            temp.Sort(new CardComparerValue());
            BestHand.Add(temp[0]);
            BestHand.Add(temp[1]);

            Console.Write("Best Hand: ");
            DisplayCards(BestHand);

        }

        static void sortPair(List<Card> cards) {
            List<Card> BestHand = new List<Card>();
            List<Card> temp = cards.ToList();
            Dictionary<Values, int> count = getCardDictionary(cards);
            Values pair = Values.Ace;

            foreach (var item in count) {
                if (item.Value == 2) {
                    pair = item.Key;
                    break;
                }
            }

            BestHand = cards.Where(c => c.Value == pair).ToList();
            temp = cards.Where(c => c.Value != pair).ToList();
            BestHand.Add(temp[0]);
            BestHand.Add(temp[1]);
            BestHand.Add(temp[2]);

            Console.Write("Best Hand: ");
            DisplayCards(BestHand);

        }

        static void sortFullHouse(List<Card> cards) {
            List<Card> BestHand = new List<Card>();
            List<Card> OtherCards = new List<Card>();
            Dictionary<Values, int> count = getCardDictionary(cards);
            Values threeOfAKind = Values.Ace;
            Values pair = Values.Ace;

            foreach (var item in count) {
                if (item.Value == 3)
                    threeOfAKind = item.Key;
                if (item.Value == 2)
                    pair = item.Key;
            }

            BestHand = cards.Where(c => c.Value == threeOfAKind || c.Value == pair).ToList();
            /*
            foreach (Card card in cards) {
                if ((card.Value == threeOfAKind) || (card.Value == pair))
                    BestHand.Add(card);
                else
                    OtherCards.Add(card);
            }
            */
            Console.Write("Best Hand: ");
            DisplayCards(BestHand);

        }

        static void sortTwoPair(List<Card> cards) {
            List<Card> BestHand = new List<Card>();
            List<Card> temp = new List<Card>();
            Dictionary<Values, int> count = getCardDictionary(cards);
            Values firstPair = Values.Ace;
            Values secondPair = Values.Ace;
            bool firstPairExists = false;
            bool secondPairExists = false;

            foreach (var item in count) {
                if ((item.Value == 2) && !firstPairExists) {
                    firstPair = item.Key;
                    firstPairExists = true;
                }
                else if ((item.Value == 2)) {
                    secondPairExists = true;
                    secondPair = item.Key;
                }
            }

            BestHand = cards.Where(c => c.Value == firstPair || c.Value == secondPair).ToList();
            temp = cards.Where(c => c.Value != firstPair || c.Value != secondPair).ToList();
            temp.Sort(new CardComparerValue());
            BestHand.Add(temp[0]);
          
            Console.Write("Best Hand: ");
            DisplayCards(BestHand);
        }

        static void sortHighCard(List<Card> cards) {
            List<Card> BestHand = new List<Card>();
            cards.Sort(new CardComparerValue());
            for (int i = 0; i < 5; i++) {
                BestHand.Add(cards[i]);
            }

            Console.Write("Best Hand: ");
            DisplayCards(BestHand);
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

        static  bool isFlush(List<Card> cards) {
            int[] count = new int[4];
            foreach (Card card in cards) {
                count[(int)card.Suit]++;
                if (count[(int)card.Suit] >= 5)
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
                else if (item.Value == 2)
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

        static bool aisStraight(List<Card> cards) {
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

        static bool isStraight(List<Card> cards) {
            cards.Sort(new CardComparerValue());
            int straightCount = 0;
            int straightValue = (int)cards[0].Value;
            int straigtValueToCheck;

            // Check for straight with Aces high
            for (int i = 1; i < cards.Count(); i++) {
                straigtValueToCheck = (int)cards[i].Value;
                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4)
                        return true;
                } else {
                    straightValue = straigtValueToCheck;
                    straightCount = 0;
                }
            }
            // Check for straight with Aces low
            cards.Sort(new CardCompareAcesLow());
            straightCount = 0;
            straightValue = ((int)cards[0].Value == 14) ? 1 : (int)cards[0].Value;
            for (int i = 1; i < cards.Count(); i++) {
                straigtValueToCheck = ((int)cards[i].Value == 14) ? 1 : (int)cards[i].Value;
                if ((straightValue - 1) == straigtValueToCheck) {
                    straightValue--;
                    straightCount++;
                    if (straightCount >= 4)
                        return true;
                } else {
                    straightValue = straigtValueToCheck;
                    straightCount = 0;
                }
            }
            return false;


        }
    }
}
