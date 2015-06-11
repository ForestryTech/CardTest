using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
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
