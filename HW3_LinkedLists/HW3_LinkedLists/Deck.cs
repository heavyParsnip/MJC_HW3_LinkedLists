using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_LinkedLists
{
    class Deck
    {
        //FIELDS
        private Card head;
        private Card tail;
        private int count;
        private Random rand;

        //PROPERTIES
        public int Count
        {
            get
            {
                Card current = head;
                int i = 0;

                while (current.Next != null)
                {
                    current = current.Next;
                    i++;
                }
                return i;
            }
        }
        public Card this[int index]
        {
            get
            {
                try
                {
                    Card current = head;
                    int i = 0;

                    if (index == 0)
                    {
                        return head;
                    }
                    else
                    {
                        while (i < index)
                        {
                            current = current.Next;
                            i++;
                        }
                        return current;
                    }
                }
                catch (System.IndexOutOfRangeException ex)
                {
                    System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
                    throw argEx;
                }
            }
            set
            {
                try
                {
                    Card current = head;
                    int i = 0;

                    if (index == 0)
                    {
                        head = value;
                    }
                    else
                    {
                        while (i < index)
                        {
                            current = current.Next;
                            i++;
                        }
                        current = value;
                    }
                }
                catch (System.IndexOutOfRangeException ex)
                {
                    System.ArgumentException argEx = new System.ArgumentException("Provided index is out of range", "index", ex);
                    throw argEx;
                }
            }
        }

        //CONSTRUCTORS

        //METHODS
        //Add a card to the end of the list
        public void Add(CardRank rank, CardSuit suit)
        {
            if(count == 0)
            {
                Card newCard = new Card(rank, suit);
                this.head = newCard;
                this.tail = newCard;
                count++;
            }
            else
            {
                Card newCard = new Card(rank, suit);
                newCard.Previous = this.tail;
                newCard.Previous.Next = newCard;
                this.tail = newCard;
                count++;
            }
        }

        //Return data value from the card at the index specified
        public Card GetData(int index)
        {
            try
            {
                return this[index];
            }
            catch(System.IndexOutOfRangeException ex)
            {
                System.ArgumentException argEx = new System.ArgumentException("Provided index is out of range", "index", ex);
                throw argEx;
            }
        }

        //Shuffle the deck by taking a number of cards off the end of the deck and placing them before a specified index
        public void Shuffle(int cardsToMove, int targetIndex)
        {
            //Say you have cards at indices 0-5; 0 is the head, 5 is the tail...
            //For the sake of the explanation, say the cards to move is 2

            //Initialize temporary simplified card references
            Card movingCard = GetData(count - 1 - cardsToMove);
            Card targetCard = GetData(targetIndex);
            
            //In the case of the example values, movingCard = 4

            //Special case: The head must be modified when the target index is 0.
            if(targetIndex == 0)
            {
                //Say the target index is 0; the original sequence 0-1-2-3-4-5 will be shuffled to 4-5-0-1-2-3
                //0.Previous now points to 5
                head.Previous = tail;
                //5.Next now points to 0
                tail.Next = head;
                //The tail is now 3
                tail = movingCard.Previous;
                //3.Next now points to null
                tail.Next = null;
                //The head is now 4
                head = movingCard;
                //4.Previous now points to null
                head.Previous = null;
            }
            //Default case
            else
            {
                //Additional temporary holders will be needed to simplify the flow of logic. In the case of the example values, targetPrev will be 1.
                Card targetPrev = GetData(targetIndex).Previous;
                Card movingPrev = movingCard.Previous;

                //Say the target index is 2; the original sequence 0-1-2-3-4-5 will be shuffled to 0-1-4-5-2-3
                //Properties that need to be modified in this example: tail.Next, tail, 1.Next, 4.Previous, 2.Previous, 5.Next
                //5.Next now points to 2
                tail.Next = targetCard;
                //4.Previous now points to 1
                movingCard.Previous = targetCard.Previous;
                //2.Previous now points to 5
                targetCard.Previous = tail;
                //The tail is now 3
                tail = movingPrev;
                //3.Next now points to null
                tail.Next = null;
                //1.Next now points to 4
                targetPrev.Next = movingCard;

                //Recursion who?
            }
            
        }

        //Deal a hand for each player
        public List<Deck> DealPlayerHands(int playerCount)
        {
            List<Deck> playerHands = new List<Deck>();
            rand = new Random();

            for(int i = 0; i < playerCount; i++)
            {
                playerHands.Add(new Deck());
            }

            //Loop for adding all cards of the deck to player hands
            Card current = head;
            int j = 0;

            while(j < count)
            {
                playerHands[rand.Next(0, playerCount)].Add(current.Rank, current.Suit);
                current = current.Next;
                j++;
            }

            return playerHands;
        }

        //Print out the deck in order
        public void Print()
        {
            Console.WriteLine($"{count} cards:");
            for(int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"\t{head.ToString()}");
                }
                else
                {
                    Console.WriteLine($"\t{this[i-1].Next.ToString()}");
                }
            }
        }

        //Print out the deck in reverse order
        public void PrintReversed()
        {
            for (int i = count-1; i >= 0; i--)
            {
                if (i == count-1)
                {
                    Console.WriteLine($"\t{tail.ToString()}");
                }
                else
                {
                    Console.WriteLine($"\t{this[i+1].Previous.ToString()}");
                }
            }
        }

        //Clear the deck
        public void Clear()
        {
            count = 0;
            head = null;
            tail = null;
        }
    }
}
