using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
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
}
