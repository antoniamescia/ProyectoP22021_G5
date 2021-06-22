using System;
using System.Collections.Generic;

namespace Library
{
    public class Account
    {
       
    //Cumple con el patrón Expert pues es el experto en la información requerida para realizar las responsabilidades otorgadas. 
    //Cumple con el patrón Creator al crear las transacciones pues usa de forma directa dichas instancias al ser el encargado de realizar las transacciones.
    // A su vez cumple con el patrón OCP, ya que es una clase que se encuentra abierta a la extensión, pero cerrada a la modificación.

        private List<Transaction> transactionsRecord;
        public Account (string name, Currency currencyType, double balance, SavingsGoal maxGoal, SavingsGoal minGoal)
        {
            this.Name = name;
            this.CurrencyType = currencyType;
            this.Balance = balance;
            this.MaxGoal = maxGoal;
            this.MinGoal = minGoal;
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

        public double Balance
        {
            get;
            private set;
        }

        public SavingsGoal MaxGoal { get; private set; }
        public SavingsGoal MinGoal { get; private set; }
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Transaction> TransactionsRecord
        {
            get
            {
                return transactionsRecord.AsReadOnly();
            }
        }

        public void Transfer(Currency currency, double ammount, string description)
        {
            Transaction transaction = new Transaction();
            this.Balance += ammount;

        }
        public void ChangeCurrencyType(Currency newCurrencyType)
        {

        }
    }
}
