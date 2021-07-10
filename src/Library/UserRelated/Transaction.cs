using System;

namespace Bankbot
{
    public class Transaction
    {
        
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }


        public Transaction(double amount, Currency currency, DateTime date, string description)
        {
            this.Amount = amount;
            this.Currency = currency;
            this.Date = date;
            this.Description = description;
        }

    }
}
