using System;

namespace BankerBot
{
    public class Transaction
    {
        /*
        Patrones y principios:
        Cumple con el patrón Expert ya que es la que contiene toda la información sobre las transacciones
        Cumple con el principio SRP por tener una unica razón de cambio.
        */
        public double Amount { get; private set; }
        public string Description { get; private set; }
        public Currency CurrencyType { get; private set; }
        public DateTime Date { get; private set; }

        /// <summary>
        /// Constructor de objeto Transacción.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="date"></param>
        /// <param name="description"></param>

        public Transaction(double amount, string description, Currency currencyType, DateTime date)
        {
            this.Amount = amount;
            this.Description = description;
            this.CurrencyType = currencyType;
            this.Date = date;
        }
    }
}
