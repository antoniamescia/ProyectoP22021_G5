using System;

namespace Library
{
    public class Transaction
    {
        public Account Account
        {
            get;
            private set;
        }
        public double Ammount
        {
            get;
            private set;
        }
        public string Description
        {
            get;
            private set;
        }
        public Currency CurrencyType
        {
            get;
            private set;
        }
        public string Date
        {
            get;
            private set;
        }
    }
}
