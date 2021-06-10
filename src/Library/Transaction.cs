using System;

namespace Library
{
    public class Transaction
    {
        //Transaction cumple con el patrón Expert ya que es la que contiene toda la información sobre las transacciones, pero tambien con el principio SRP por tener una unica razón de cambio.
        public Account Account
        {
            get;
            private set;
        }
        public double Amount
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
