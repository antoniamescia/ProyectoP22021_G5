using System;
using System.Collections.Generic;

namespace Library
{
    public class Account
    {
        private List<Transaction> transactionsRecord;
        public Account (string name, Currency currencyType)
        {
            this.Name = name;
            this.CurrencyType = currencyType;
        }

        public string Name
        {
            get;
            set;
        }
        public Currency CurrencyType
        {
            get;
            private set;
        }
        public int Amount
        {
            get;
            private set;
        }
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Transaction> TransactionsRecord
        {
            get
            {
                return transactionsRecord.AsReadOnly();
            }
        }

        public void Transfer(Transaction transaction)
        {

        }
        public void ChangeCurrencyType(Currency newCurrencyType)
        {

        }
    }
}
