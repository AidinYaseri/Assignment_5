using System.Security.Cryptography.X509Certificates;

namespace Assignment_5
{
    public class Card
    {
        private string _rank;
        private string _suit;
        private string _color;
        private int _cardNumber;

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
            this.rank = rank;
            this.suit = suit;
        }
        Card(string color)
        {
            this.color = color;
        }
        Card(int cardNum)
        {
            _cardNumber = cardNum;
        }


    }

    public class Deck
    {
       

    }
 

    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
