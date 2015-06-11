using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
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
}
