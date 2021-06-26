using System;
using System.Collections.Generic;

namespace Library
{
    public class Account
    {

        /*
        Patrones y principios:
        Cumple con SRP porque no se identifica más de un razón de cambio.
        Cumple con el patrón Expert pues es el experto en la información requerida para realizar las responsabilidades otorgadas. 
        Cumple con el patrón Creator al crear las transacciones pues usa de forma directa dichas instancias al ser el encargado de realizar las transacciones.
        */
    

        private List<Transaction> transactionsRecord;
        public string Name { get; set; }
        public Currency CurrencyType { get; private set; }
        public double Amount { get; private set; }
        public SavingsGoal MaxGoal { get; private set; }
        public SavingsGoal MinGoal { get; private set; }

        public Account(string name, Currency currencyType, double amount, SavingsGoal maxGoal, SavingsGoal minGoal)   // hay que decidir si siempre tiene una SavingsGoal o creamos metodos para agregarle cuando querramos
        {
            this.transactionsRecord = new List<Transaction>();
            this.Name = name;
            this.CurrencyType = currencyType;
            this.Amount = amount;
            this.MaxGoal = maxGoal;
            this.MinGoal = minGoal;
        }

        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Transaction> TransactionsRecord
        {
            get
            {
                return transactionsRecord.AsReadOnly();
            }
        }

        public void Transfer(Currency currency, double amount, string description)
        {
            Transaction transaction = new Transaction(amount, description, currency, DateTime.Now);
            this.transactionsRecord.Add(transaction);
            this.Amount += amount;
        }
        public void ChangeCurrencyType(Currency newCurrencyType)        //se necesita currency exchanger 
        {

        }
        public void ChangeMaxGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
        {
            SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
            this.MaxGoal = newGoal;
        }
        public void ChangeMinGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
        {
            SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
            this.MinGoal = newGoal;
        }
    }
}
