﻿using System.Security.Cryptography.X509Certificates;

namespace Assignment_5
{
    public class Card
    {
    }

    public class Deck
    {
        private string _rank;
        private string _suit;
        private string _color;
        
         string rank
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

    }
 

    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
