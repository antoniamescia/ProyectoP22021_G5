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

        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Constructor de objeto Transacción 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="date"></param>
        /// <param name="description"></param>
        public Transaction(double amount, Currency currency, DateTime date, string description)
        {
            this.Amount = amount;
            this.Currency = currency;
            this.Date = date;
            this.Description = description;
        }

    }
}
