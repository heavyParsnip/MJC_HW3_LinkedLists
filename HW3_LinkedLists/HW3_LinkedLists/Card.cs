using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_LinkedLists
{
    class Card
    {
        //FIELDS
        private Card next;
        private Card previous;
        private CardRank rank;
        private CardSuit suit;

        //PROPERTIES
        public Card Next
        {
            get { return next; }
            set { next = value; }
        }
        public Card Previous
        {
            get { return previous; }
            set { previous = value; }
        }
        public CardRank Rank
        {
            get { return rank; }
        }
        public CardSuit Suit
        {
            get { return suit; }
        }
        
        //CONSTRUCTORS
        public Card(CardRank rank, CardSuit suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        //METHODS
        public override string ToString()
        {
            return $"{rank} of {suit}";
        }

    }
}
