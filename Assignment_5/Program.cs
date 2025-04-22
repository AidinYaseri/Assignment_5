using System;
using System.Text;
using System.Collections.Generic;

namespace Assignment_5
{
    /*
    * Programming 2 - Assignment 5 – Winter 2025
    * Created by: Aidin Yaseri 2467917
    * Tested by: Omid
    * Relationship: Father
    * Date: 2025-04-17
    *
    * Description: The goal of this program is to simulate a card game with different game states, handling
    * the creation of cards, decks, and player hands. It allows for shuffling, dealing cards, and maintaining 
    * the game state, including discard piles and player hands.
    */

    public class Card
    {
        private string _rank;  // Field to store the card's rank (e.g., "Ace", "2", "King")
        private string _suit;  // Field to store the card's suit (e.g., "hearts", "spades")
        private string _color; // Field to store the card's color (e.g., "red", "black")
        private int _cardNumber; // Field to store the card number (for handling jokers)

        // Property to get or set the card's rank
        public string Rank
        {
            get
            {
                return _rank; // Return the rank of the card
            }
            set
            {
                _rank = value; // Set the rank of the card
            }
        }

        // Property to get or set the card's suit
        public string Suit
        {
            get
            {
                return _suit; // Return the suit of the card
            }
            set
            {
                _suit = value; // Set the suit of the card
            }
        }

        // Property to get or set the card's color
        public string Color
        {
            get
            {
                return _color; // Return the color of the card
            }
            set
            {
                _color = value; // Set the color of the card
            }
        }

        // Constructor to create a card using a rank and suit
        public Card(string rank, string suit)
        {
            this.Rank = rank.ToLower(); // Convert rank to lowercase to standardize
            this.Suit = suit.ToLower(); // Convert suit to lowercase to standardize

            // Validate that the suit is one of the valid suits (hearts, diamonds, spades, clubs)
            if (suit == "hearts" || suit == "diamonds")
            {
                Color = "red"; // If suit is hearts or diamonds, assign red color
            }
            else if (suit == "spades" || suit == "clubs")
            {
                Color = "black"; // If suit is spades or clubs, assign black color
            }
            else
                throw new Exception("The suit provided is not one in a deck of cards"); // Throw an error if suit is invalid

            // Validate that the rank is valid (either face cards or numbers 1-11)
            if (rank != "ace" && rank != "king" && rank != "queen" && rank != "jack" && rank != "joker" &&
                (!int.TryParse(rank, out int number) || number < 1 || number > 11))
            {
                throw new Exception("The rank provided is not one in a deck of cards"); // Throw an error if rank is invalid
            }
        }

        // Constructor to create a joker card (black or red)
        public Card(string color)
        {
            color = color.ToLower(); // Convert color to lowercase
            if (color != "black" && color != "red")
            {
                throw new Exception("Joker must be either black or red"); // Throw an error if color is invalid
            }
            Rank = "joker"; // Set the rank to joker
            this.Color = color; // Set the color for the joker
            Suit = null; // Jokers do not have a suit
        }

        // Constructor to create a card from a card number (1-54)
        public Card(int cardNum)
        {
            if (cardNum < 1 || cardNum > 54)
            {
                throw new Exception("The card provided must be between 1 and 54"); // Throw an error if card number is out of range
            }

            // Check if the card number is for a joker (53 or 54)
            if (cardNum == 53)
            {
                Rank = "joker"; // Assign rank "joker"
                Suit = null; // No suit for a joker
                Color = "red"; // Red color for joker
            }
            else if (cardNum == 54)
            {
                Rank = "joker"; // Assign rank "joker"
                Suit = null; // No suit for a joker
                Color = "black"; // Black color for joker
            }
            else
            {
                // Use modulus and division to determine the suit and rank
                string[] suits = { "clubs", "diamonds", "hearts", "spades" };
                string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

                int suitIndex = (cardNum - 1) / 13; // Calculate the suit index
                int rankIndex = (cardNum - 1) % 13; // Calculate the rank index

                Rank = ranks[rankIndex]; // Assign the rank
                Suit = suits[suitIndex]; // Assign the suit

                // Determine the color based on the suit
                if (Suit == "hearts" || Suit == "diamonds")
                {
                    Color = "red"; // Red for hearts and diamonds
                }
                else
                {
                    Color = "black"; // Black for clubs and spades
                }
            }
            _cardNumber = cardNum; // Store the card number
        }

        // Override the ToString() method to provide a readable card representation
        public override string ToString()
        {
            Console.OutputEncoding = Encoding.UTF8; // Ensure proper encoding for special characters

            if (Rank == "joker") // If the card is a joker, return a specific string
            {
                if (Color == "red")
                    return ("Red Joker");
                else
                    return ("Black Joker");
            }

            string symbolSuit = "";
            // Map the suit to its corresponding symbol
            switch (Suit)
            {
                case "clubs":
                    symbolSuit = "♣"; // Clubs symbol
                    break;
                case "diamonds":
                    symbolSuit = "♦"; // Diamonds symbol
                    break;
                case "hearts":
                    symbolSuit = "♥"; // Hearts symbol
                    break;
                case "spades":
                    symbolSuit = "♠"; // Spades symbol
                    break;
            }

            // Return a string with the rank and suit symbol
            return (Rank + symbolSuit);
        }

        // Override Equals() to compare two cards for equality based on rank, suit, and color
        public override bool Equals(object otherCard)
        {
            if (otherCard == null)
                return false; // Return false if otherCard is null

            if (otherCard is Card newCard)
            {
                // Compare the rank, suit, and color of the two cards
                if (this.Suit == newCard.Suit && this.Rank == newCard.Rank && this.Color == newCard.Color)
                {
                    return true; // Return true if all the properties match
                }
            }
            return false; // Return false if cards are not equal
        }

        // Override GetHashCode() to generate a unique hash code based on the card's properties
        public override int GetHashCode()
        {
            int cardHash = 17; // Start with a common prime number
            cardHash = cardHash * 31 + (Rank != null ? Rank.GetHashCode() : 0); // Add the rank's hash
            cardHash = cardHash * 31 + (Suit != null ? Suit.GetHashCode() : 0); // Add the suit's hash
            cardHash = cardHash * 31 + (Color != null ? Color.GetHashCode() : 0); // Add the color's hash

            return cardHash; // Return the final hash code
        }
    }

    public class Deck
    {
        private List<Card> cards; // List to hold all cards in the deck

        // Getter for the number of cards left in the deck
        public int CardsLeft
        {
            get
            {
                return cards.Count; // Return the count of cards remaining in the deck
            }
        }

        // Default constructor to create an empty deck
        public Deck()
        {
            cards = new List<Card>(); // Initialize an empty list of cards
        }

        // Constructor that optionally includes Jokers
        public Deck(bool hasJoker)
        {
            cards = new List<Card>(); // Initialize an empty list of cards
            DeckGenerator(); // Generate a standard deck of cards
            if (hasJoker) // If Jokers are included, add them to the deck
            {
                cards.Add(new Card("red"));
                cards.Add(new Card("black"));
            }
        }

        // Constructor that accepts the number of decks to create
        public Deck(int numberOfDecks)
        {
            cards = new List<Card>(); // Initialize an empty list of cards
            for (int decks = 0; decks < numberOfDecks; decks++) // Loop to create multiple decks
            {
                DeckGenerator(); // Generate a standard deck for each iteration
            }
        }

        // Helper method to generate a standard deck of cards (no Jokers)
        private void DeckGenerator()
        {
            string[] suits = { "clubs", "diamonds", "hearts", "spades" }; // List of valid suits
            string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" }; // List of valid ranks

            // Loop through each suit
            foreach (string suit in suits)
            {
                // Loop through each rank
                foreach (string rank in ranks)
                {
                    cards.Add(new Card(rank, suit)); // Add each card to the deck
                }
            }
        }

        // Override ToString() to return a message with the number of cards left in the deck
        public override string ToString()
        {
            return $"Deck has {cards.Count} card(s) remaining."; // Return a message with the remaining card count
        }

        // Method to draw a card from the deck
        public Card Draw()
        {
            if (cards.Count == 0)
                throw new Exception("Cannot draw due to empty deck"); // Throw an error if deck is empty

            Card drawnCard = cards[0]; // Get the top card from the deck
            cards.RemoveAt(0); // Remove the top card from the deck
            return drawnCard; // Return the drawn card
        }

        // Method to peek at the top card of the deck without removing it
        public Card Peek()
        {
            if (cards.Count == 0)
                throw new Exception("Cannot peek due to empty deck"); // Throw an error if deck is empty

            return cards[0]; // Return the top card of the deck
        }

        // Method to insert a card at the top of the deck
        public void InsertCard(Card cardToInsert)
        {
            cards.Insert(0, cardToInsert); // Insert the card at the top of the deck
        }

        // Method to shuffle the deck randomly
        public void Shuffle()
        {
            Random random = new Random(); // Create a random number generator

            // Loop to shuffle the deck by swapping cards
            for (int numOfCard = cards.Count - 1; numOfCard > 1; numOfCard--)
            {
                int randIndex = random.Next(numOfCard + 1); // Generate a random index
                // Swap the positions of two cards in the deck
                (cards[randIndex], cards[numOfCard]) = (cards[numOfCard], cards[randIndex]);
            }
        }
    }

    // Hand class represents the collection of cards held by a player
    public class Hand
    {
        private List<Card> hand; // List to hold cards in the hand
        private string[] suitsPriority = new string[4]; // Array to hold the priority order of suits

        // Property to return the size of the hand (number of cards)
        public int Size
        {
            get
            {
                return hand.Count; // Return the number of cards in the hand
            }
        }

        // Constructor to initialize a hand with suit priority
        public Hand(string[] suits)
        {
            if (suits.Length != 4)
                throw new Exception("There must be 4 suits in the priority suits"); // Ensure there are exactly 4 suits

            this.suitsPriority = suits; // Set the suit priority
            this.hand = new List<Card>(); // Initialize an empty list of cards for the hand
        }

        // Override ToString() to return a string representing the hand
        public override string ToString()
        {
            return string.Join(", ", hand.Select(card => card.ToString())); // Return a comma-separated list of cards in the hand
        }

        // Method to add a card to the hand
        public void AddCard(Card card)
        {
            hand.Add(card); // Add the card to the hand
            OrderBySuit(); // Sort the hand by suit
        }

        // Method to remove a card from the hand
        public void RemoveCard(Card card)
        {
            if (hand.Count == 0)
                throw new Exception("Cannot remove card from an empty hand"); // Ensure the hand is not empty before removal

            hand.Remove(card); // Remove the specified card from the hand
            OrderBySuit(); // Sort the hand by suit after removal
        }

        // Method to check if the hand contains a specific card
        public bool Contains(Card card)
        {
            foreach (Card handCard in hand)
            {
                if (card.Equals(handCard)) // Check if the card is in the hand
                {
                    return true; // Return true if card is found
                }
            }
            return false; // Return false if card is not found
        }

        // Private helper method to order the cards by suit and move jokers to the end
        private void OrderBySuit()
        {
            List<Card> normalCards = new List<Card>(); // List to hold non-joker cards
            List<Card> jokerCards = new List<Card>(); // List to hold joker cards

            // Separate jokers and normal cards
            foreach (Card card in hand)
            {
                if (card.Rank != "joker")
                {
                    normalCards.Add(card);
                }
                else
                    jokerCards.Add(card);
            }

            // Sort the normal cards by suit priority
            normalCards.Sort((card1, card2) => Array.IndexOf(suitsPriority, card1.Suit).CompareTo(Array.IndexOf(suitsPriority, card2.Suit)));

            normalCards.AddRange(jokerCards); // Add jokers at the end
            hand = normalCards; // Update the hand with the sorted cards
        }
    }

    // GameState class represents the state of the game, including decks, discard piles, and player hands
    public class GameState
    {
        private Deck drawDeck; // The deck of cards from which cards are drawn
        private Deck discardPile; // The pile of discarded cards
        private List<Hand> playerHands; // List of hands for each player
        private string[] suitsPriority = new string[4]; // Array to hold the priority order of suits

        // Property to return the number of players
        public int NumberOfPlayers
        {
            get
            {
                return playerHands.Count; // Return the number of players
            }
        }

        // Property to return the count of remaining cards in the draw deck
        public int DrawDeckCount
        {
            get
            {
                return drawDeck.CardsLeft; // Return the number of cards left in the draw deck
            }
        }

        // Property to return the count of remaining cards in the discard pile
        public int DiscardPileCount
        {
            get
            {
                return discardPile.CardsLeft; // Return the number of cards left in the discard pile
            }
        }

        // Constructor to initialize the game with the number of players, jokers, and suit priority
        public GameState(int numOfPlayers, bool hasJokers, string[] suits)
        {
            if (suits.Length != 4)
                throw new Exception("Suit priority must contain exactly 4 suits"); // Ensure there are exactly 4 suits

            this.suitsPriority = suits; // Set the suit priority

            drawDeck = new Deck(hasJokers); // Initialize the draw deck
            drawDeck.Shuffle(); // Shuffle the deck

            discardPile = new Deck(); // Initialize the discard pile

            playerHands = new List<Hand>(); // Initialize the list of player hands
            for (int i = 0; i < numOfPlayers; i++)
            {
                playerHands.Add(new Hand(suitsPriority)); // Create a hand for each player
            }
        }

        // Method to deal a specified number of cards to each player
        public void Deal(int cardsPerPlayer)
        {
            for (int i = 0; i < cardsPerPlayer; i++) // Deal cards one by one to each player
            {
                foreach (Hand hand in playerHands)
                {
                    if (drawDeck.CardsLeft > 0)
                    {
                        hand.AddCard(drawDeck.Draw()); // Draw a card from the draw deck and add to the player's hand
                    }
                }
            }
        }

        // Method to discard a card to the discard pile
        public void Discard(Card card)
        {
            discardPile.InsertCard(card); // Insert the card into the discard pile
        }

        // Override ToString() to display the current game state
        public override string ToString()
        {
            Console.OutputEncoding = Encoding.UTF8; // Ensure proper encoding for special characters
            string output = ""; // Initialize the output string

            output += "Player Hands:\n";
            int playerNumber = 1;
            foreach (Hand hand in playerHands) // Display each player's hand
            {
                output += $"Player {playerNumber}: {hand}\n";
                playerNumber++;
            }

            output += $"\nDraw Deck: {drawDeck.CardsLeft} card(s) remaining\n";

            if (discardPile.CardsLeft > 0)
            {
                output += $"Discard Pile: {discardPile.CardsLeft} card(s). Top Card: {discardPile.Peek()}\n";
            }
            else
            {
                output += "Discard Pile: 0 card(s). Top Card: None\n";
            }

            return output; // Return the game state string
        }

        // Method to deal cards with reshuffling if the draw deck is empty
        public void DealWithReshuffle(int cardsPerPlayer)
        {
            for (int i = 0; i < cardsPerPlayer; i++) // Deal cards to each player
            {
                foreach (Hand hand in playerHands)
                {
                    if (drawDeck.CardsLeft == 0 && discardPile.CardsLeft > 0) // If the draw deck is empty and there are cards in the discard pile
                    {
                        MoveDiscardToDeck(); // Move cards from the discard pile to the draw deck
                        drawDeck.Shuffle(); // Shuffle the new draw deck
                        Console.WriteLine("Deck was empty. Discard pile shuffled back into deck.");
                    }

                    if (drawDeck.CardsLeft > 0)
                    {
                        hand.AddCard(drawDeck.Draw()); // Draw a card from the shuffled draw deck
                    }
                    else
                    {
                        Console.WriteLine("No more cards available to continue dealing.");
                        return; // Stop dealing if there are no cards left
                    }
                }
            }
        }

        // Private helper method to move all cards from the discard pile to the draw deck
        private void MoveDiscardToDeck()
        {
            while (discardPile.CardsLeft > 0) // While there are cards in the discard pile
            {
                drawDeck.InsertCard(discardPile.Draw()); // Draw a card from the discard pile and insert it into the draw deck
            }
        }
    }

    // Main program class with game loop and options
    internal class Program
    {
        static GameState game = null; // Initialize the game state object

        // Main method to start the game
        static void Main(string[] args)
        {
            bool quit = false; // Flag to control the game loop

            while (!quit)
            {
                Console.Clear(); // Clear the console screen
                Console.WriteLine("************************************");
                Console.WriteLine("Welcome to Programming 2 - Assignment 5 – Winter 2025");
                Console.WriteLine("Created by Aidin Yaseri (2467917) on 2025-04-17");
                Console.WriteLine("************************************");
                Console.WriteLine("\n===== Main Menu =====");
                Console.WriteLine("1. Setup Game");
                Console.WriteLine("2. Deal Hands");
                Console.WriteLine("3. Display Gameboard");
                Console.WriteLine("4. Quit");
                Console.Write("Select an option (1-4): ");

                string input = Console.ReadLine(); // Get user input

                // Handle user input for menu options
                switch (input)
                {
                    case "1":
                        SetupGame(); // Setup a new game
                        break;
                    case "2":
                        DealHands(); // Deal cards to players
                        break;
                    case "3":
                        DisplayGameboard(); // Display the current game state
                        break;
                    case "4":
                        quit = true; // Exit the game
                        Console.WriteLine("Quitting the game.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please choose between 1 and 4.");
                        break;
                }
            }
        }

        // Method to setup the game (number of players, suits, jokers)
        static void SetupGame()
        {
            try
            {
                int numPlayers = 0; // Initialize the number of players
                while (numPlayers <= 0)
                {
                    Console.Write("Enter number of players (positive integer): ");
                    if (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid positive number.");
                    }
                }

                bool hasJokers = false; // Flag to include jokers
                while (true)
                {
                    Console.Write("Include jokers? (yes/no): ");
                    string jokerInput = Console.ReadLine().ToLower(); // Get user input
                    if (jokerInput == "yes")
                    {
                        hasJokers = true; // Set the flag to true if jokers are included
                        break;
                    }
                    else if (jokerInput == "no")
                    {
                        hasJokers = false; // Set the flag to false if jokers are not included
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please type 'yes' or 'no'."); // Ask for valid input
                    }
                }

                // Define valid suits
                string[] validSuits = { "spades", "hearts", "clubs", "diamonds" };
                string[] suitsPriority;

                // Validate the suits entered by the user
                while (true)
                {
                    Console.WriteLine("Enter suit priority (comma separated, e.g., spades,hearts,clubs,diamonds): ");
                    suitsPriority = Console.ReadLine().ToLower().Replace(" ", "").Split(','); // Get and format user input

                    if (suitsPriority.Length == 4 && AllValidSuits(suitsPriority, validSuits))
                    {
                        break; // Break if valid suits are entered
                    }
                    else
                    {
                        Console.WriteLine("Invalid suits. Make sure to enter exactly 4 valid suits in any order.");
                    }
                }

                // Create the game state object
                game = new GameState(numPlayers, hasJokers, suitsPriority);
                Console.WriteLine("Game has been successfully set up."); // Notify user that the game is set up
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setup failed: " + ex.Message); // Handle any errors during setup
            }
        }

        // Method to deal cards to players
        static void DealHands()
        {
            if (game == null)
            {
                Console.WriteLine("You must setup the game before dealing cards.");
                return; // Return if the game isn't set up
            }

            try
            {
                Console.Write("Enter number of cards per player: ");
                int cardsPerPlayer = int.Parse(Console.ReadLine());

                int totalNeeded = cardsPerPlayer * game.NumberOfPlayers;
                int totalAvailable = game.DrawDeckCount + game.DiscardPileCount;

                if (totalNeeded > totalAvailable)
                {
                    Console.WriteLine("Not enough cards to deal. Please setup the game again.");
                    return;
                }

                game.DealWithReshuffle(cardsPerPlayer);
                Console.WriteLine("Cards dealt to all players.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while dealing: " + ex.Message); // Handle errors during dealing
            }
        }

        // Method to display the current game state
        static void DisplayGameboard()
        {
            if (game == null)
            {
                Console.WriteLine("You must setup the game first.");
                return; // Return if the game isn't set up
            }

            Console.WriteLine("===== Gameboard =====");
            Console.WriteLine(game.ToString()); // Display the game state
            Console.ReadKey(); 
        }

        // Helper method to validate the suits entered by the user
        static bool AllValidSuits(string[] inputSuits, string[] validSuits)
        {
            foreach (string suit in inputSuits)
            {
                if (Array.IndexOf(validSuits, suit) == -1)
                    return false; // Return false if any of the suits is invalid
            }
            return true; // Return true if all suits are valid
        }
    }
}
