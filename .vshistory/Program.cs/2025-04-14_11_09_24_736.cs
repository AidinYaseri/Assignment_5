﻿using System.Security.Cryptography.X509Certificates;

namespace Assignment_5
{
    public class Card
    {
        private string _rank;
        private string _suit;
        private string _color;

        public string rank
        {
            get
            {
                return _rank;
            }
        }
        string suit
        {
            get
            {
                return _suit;
            }
        }
        public string color
        {
            get
            {
                return _color;
            }
        }

        Card(string cardRank, string suit)
        {
            _rank = cardRank;
            _suit = suit;
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
