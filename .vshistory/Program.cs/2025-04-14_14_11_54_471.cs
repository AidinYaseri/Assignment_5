using System.Runtime.CompilerServices;
using System.Text;

namespace Assignment_5
{
    public class Card
    {
        private string _rank;
        private string _suit;
        private string _color;
        private int _cardNumber;

        // Making setters for the relevant fields
        public string Rank
        {
            get
            {
                return _rank;
            }
            set
            {

            }
        }
        string Suit
        {
            get
            {
                return _suit;
            }
            set
            {

            }
        }
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {

            }
        }

        public Card(string rank, string suit)
        {
            // turning rank and suit to lower case
            this.Rank = rank.ToLower();
            this.Suit = suit.ToLower();

            // verifing that the inputs are valid 
            if (suit == "hearts" || suit == "diamonds")
            {
                Color = "red";

            }
            else if (suit == "spades" || suit == "clubs")
            {
                Color = "black";
            }
            else
                throw new Exception("The suit provided in not one in a deck of cards"); // If not correct, throw exception

            if (rank != "ace" || rank != "king" || rank != "queen" || rank != "jack" || rank != "joker" || int.Parse(rank) < 1 || int.Parse(rank) > 11)
            {
                throw new Exception("The rank provided in not one in a deck of cards"); // If not correct, throw exception
            }

        }
        public Card(string color)
        {
            // turning the color to lowecase
            color = color.ToLower();
            // verifing input for correct color
            if (color != "black" || color != "red")
            {
                throw new Exception("Joker must be either black or red"); // If not correct, throw exception
            }
            // setting correct card info
            Rank = "joker";
            this.Color = color;
            Suit = null;
        }
        public Card(int cardNum)
        {
            // validating to see if number is in the right bound
            if (cardNum < 1 || cardNum > 54)
            {
                throw new Exception("The card provided must be between 1 and 54"); // If not correct, throw exception
            }
            // looking for joker and setting fields accordantly
            if (cardNum == 53)
            {
                Rank = "joker";
                Suit = null;
                Color = "red";
            }
            else if (cardNum == 54)
            {
                Rank = "joker";
                Suit = null;
                Color = "black";
            }
            else
            {
                // converting number to suit, rank and color and setting fields accordantly
                string[] suits = { "clubs", "diamonds", "hearts", "spades" };
                string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

                int suitIndex = (cardNum - 1) / 13;
                int rankIndex = (cardNum - 1) % 13; //using modulos

                Rank = ranks[rankIndex];
                Suit = suits[suitIndex];
                // determining the color
                if (Rank == "hearts" || Rank == "diamonds")
                {
                    Color = "red";
                }
                else
                    Color = "black";

            }
            _cardNumber = cardNum;
        }
        public override string ToString()
        {
            // command for the symbols to appear
            Console.OutputEncoding = Encoding.UTF8;

            // checking and returning for a joker
            if (Rank == "joker")
            {
                if (Suit == "red")
                    return ("Red Joker");
                else
                    return ("Black Joker");
            }

            string symbolSuit = "";
            // switching string suits to symbols
            switch (Suit)
            {
                case "clubs":
                    symbolSuit = "♣";
                    break;
                case "diamonds":
                    symbolSuit = "♦";
                    break;
                case "hearts":
                    symbolSuit = "♥";
                    break;
                case "spades":
                    symbolSuit = "♠";
                    break;
            }
            // returning the card
            return (Rank + symbolSuit);
        }

        public override bool Equals(object otherCard)
        {
            // checking if otherCard contains info 
            if (otherCard == null)
                return false; // returning false if otherCard is empty

            if (otherCard is Card newCard)
            {
                //checking for equality between the two cards
                if (this.Suit == newCard.Suit && this.Rank == newCard.Rank && this.Color == newCard.Color)
                {
                    return true; // returning true if all characters are a match
                }

            }
            return false; // if no matches return false

        }
        public override int GetHashCode()
        {
            int cardHash = 17; // starting by a common prime number
            // combining the hashes
            cardHash = cardHash * 31 + (Rank != null ? Rank.GetHashCode() : 0);
            cardHash = cardHash * 31 + (Suit != null ? Suit.GetHashCode() : 0);
            cardHash = cardHash * 31 + (Color != null ? Color.GetHashCode() : 0);

            return cardHash; // returning the final hash

        }

    }

    public class Deck
    {

        private List<Card> cards;
        public int CardsLeft
        {
            set
            {
                cards.Count();
            }
        }

        public Deck()
        {
            cards = new List<Card>();
        }
        public Deck(bool hasJoker)
        {
            cards = new List<Card>();

            string[] suits = { "clubs", "diamonds", "hearts", "spades" };
            string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            if (hasJoker)
            {
                cards.Add(new Card("red"));
                cards.Add(new Card("black"));
            }

        }
        public Deck(int numberOfDecks)
        {
            for (int decks = 0; decks >= numberOfDecks; decks++)
            {
                {
                    string[] suits = { "clubs", "diamonds", "hearts", "spades" };
                    string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

                    foreach (string suit in suits)
                    {
                        foreach (string rank in ranks)
                        {
                            cards.Add(new Card(rank, suit));
                        }
                    }

                    if (hasJoker)
                    {
                        cards.Add(new Card("red"));
                        cards.Add(new Card("black"));
                    }

                    
                }
            }
        }
        private List<Card> DeckGenerator()
        {
            string[] suits = { "clubs", "diamonds", "hearts", "spades" };
            string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            if (hasJoker)
            {
                cards.Add(new Card("red"));
                cards.Add(new Card("black"));
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
