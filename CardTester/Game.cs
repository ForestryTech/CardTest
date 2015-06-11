using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
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
}
