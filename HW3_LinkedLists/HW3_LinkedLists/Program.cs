using System;
using System.Collections.Generic;

namespace HW3_LinkedLists
{
    enum CardSuit
    {
        Hearts,
        Spades,
        Diamonds,
        Clubs
    }

    enum CardRank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    // DO NOT MODIFY ANY CODE IN THIS FILE
    // You can comment out sections that don't compile/run
    class Program
    {
        // Creates a test list and run a user input loop to allow the list to be
        // modified
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Linked List homework!\n\n");

            Deck myDeck = new Deck();

            Random rand = new Random();

            int playerCount;
            List<Deck> playerHands = new List<Deck>();

            // Main user loop
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("add\n" +
                    "shuffle\n" +
                    "deal\n" +
                    "print\n" +
                    "backwards\n" +
                    "clear\n" +
                    "quit\n" +
                    "\nWhat would you like to do?");
                string input = Console.ReadLine();

                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        case "clear":
                            myDeck.Clear();
                            break;
                        case "add":
                            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                            {
                                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                                {
                                    myDeck.Add(rank, suit);
                                }
                            }
                            break;
                         case "shuffle":
                            int overhandTimes = rand.Next(100, 1001);
                            Console.WriteLine("Shuffle {0} times", overhandTimes);

                            for (int i = 0; i < overhandTimes; ++i)
                            {
                                int cardsToMove = rand.Next(myDeck.Count / 2);
                                int indexLimt = myDeck.Count - (cardsToMove + 1);

                                if(indexLimt < 1)
                                {
                                    throw new IndexOutOfRangeException("Invalid index: " + indexLimt);
                                }

                                int targetIndex = rand.Next(1, indexLimt);

                                myDeck.Shuffle(cardsToMove, targetIndex);
                            }
                            break;
                        case "deal":
                            Console.WriteLine("How many players are there?");

                            if (int.TryParse(Console.ReadLine(), out playerCount)
                                && playerCount > 0)
                            {
                                playerHands = myDeck.DealPlayerHands(playerCount);

                                for (int i = 0; i < playerHands.Count; ++i)
                                {
                                    Console.WriteLine("Player {0} hand:", i + 1);

                                    playerHands[i].Print();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Sorry that wasn't a vaild number");
                            }
                            break;
                        case "print":
                            myDeck.Print();
                            break;
                        case "backwards":
                            myDeck.PrintReversed();
                            break;
                        case "quit":
                            Console.WriteLine("Goodbye");
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Sorry that was an invalid command.");
                            break;
                    }
                }
                // Main() ONLY handles an IndexOutOfRangeException exception (which your custom
                // linked list must throw when appropriate). Any other exception type will
                // still cause the program to crash.
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Error handling command \"" + input + "\" - " + e.Message);
                }

                Console.WriteLine("\n\n\n");
            }
        }
    }
}
