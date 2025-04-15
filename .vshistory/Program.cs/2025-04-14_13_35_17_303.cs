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
        public string rank
        {
            get
            {
                return _rank;
            }
            set
            {

            }
        }
        string suit
        {
            get
            {
                return _suit;
            }
            set
            {

            }
        }
        public string color
        {
            get
            {
                return _color;
            }
            set
            {

            }
        }

        Card(string rank, string suit)
        {
            // turning rank and suit to lower case
            this.rank = rank.ToLower();
            this.suit = suit.ToLower();

            // verifing that the inputs are valid 
            if (suit == "hearts" || suit == "diamonds")
            {
                color = "red";

            }
            else if (suit == "spades" || suit == "clubs")
            {
                color = "black";
            }
            else
                throw new Exception("The suit provided in not one in a deck of cards"); // If not correct, throw exception

            if (rank != "ace" || rank != "king" || rank != "queen" || rank != "jack" || rank != "joker" || int.Parse(rank) < 1 || int.Parse(rank) > 11)
            {
                throw new Exception("The rank provided in not one in a deck of cards"); // If not correct, throw exception
            }

        }
        Card(string color)
        {
            // turning the color to lowecase
            color = color.ToLower();
            // verifing input for correct color
            if (color != "black" || color != "red")
            {
                throw new Exception("Joker must be either black or red"); // If not correct, throw exception
            }
            // setting correct card info
            rank = "joker";
            this.color = color;
            suit = null;
        }
        Card(int cardNum)
        {
            // validating to see if number is in the right bound
            if (cardNum < 1 || cardNum > 54)
            {
                throw new Exception("The card provided must be between 1 and 54"); // If not correct, throw exception
            }
            // looking for joker and setting fields accordantly
            if (cardNum == 53)
            {
                rank = "joker";
                suit = null;
                color = "red";
            }
            else if (cardNum == 54)
            {
                rank = "joker";
                suit = null;
                color = "black";
            }
            else
            {
                // converting number to suit, rank and color and setting fields accordantly
                string[] suits = { "clubs", "diamonds", "hearts", "spades" };
                string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

                int suitIndex = (cardNum - 1) / 13;
                int rankIndex = (cardNum - 1) % 13; //using modulos

                rank = ranks[rankIndex];
                suit = suits[suitIndex];
                // determining the color
                if (rank == "hearts" || rank == "diamonds")
                {
                    color = "red";
                }
                else
                    color = "black";

            }
            _cardNumber = cardNum;
        }
        public override string ToString()
        {
            // command for the symbols to appear
            Console.OutputEncoding = Encoding.UTF8;

            // checking and returning for a joker
            if (rank == "joker")
            {
                if (suit == "red")
                    return ("Red Joker");
                else
                    return ("Black Joker");
            }

            string symbolSuit = "";
            // switching string suits to symbols
            switch (suit)
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
            return (rank + symbolSuit);
        }

        public override bool Equals(object otherCard)
        {
            // checking if otherCard contains info 
            if (otherCard == null)
                return false; // returning false if otherCard is empty

            if (otherCard is Card newCard)
            {
                //checking for equality between the two cards
                if (this.suit == newCard.suit && this.rank == newCard.rank && this.color == newCard.color)
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
            cardHash = cardHash * 31 + (rank != null ? rank.GetHashCode() : 0);
            cardHash = cardHash * 31 + (suit != null ? suit.GetHashCode() : 0);
            cardHash = cardHash * 31 + (color != null ? color.GetHashCode() : 0);

            return cardHash; // returning the final hash

        }

    }

    public class Deck
    {

        private List<Card> _cards;
    }


    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
