using System;
using System.Collections.Generic;

namespace BankerBot
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

        public Account(string name, Currency currencyType, double amount, SavingsGoal maxGoal, SavingsGoal minGoal)
        {
            this.transactionsRecord = new List<Transaction>();
            this.Name = name;
            this.CurrencyType = currencyType;
            this.Amount = amount;
            this.MaxGoal = maxGoal;
            this.MinGoal = minGoal;
        }

        public IList<Transaction> TransactionsRecord
        {
            get
            {
                return transactionsRecord.AsReadOnly();
            }
        }
        /// <summary>
        /// Realiza una transacción ya sea de ingreso o egreso.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public void Transfer(Currency currency, double amount, string description)
        {
            Transaction transaction = new Transaction(amount, description, currency, DateTime.Now);
            this.transactionsRecord.Add(transaction);
            this.Amount += amount;
        }

        /// <summary>
        /// Cambia el tipo de moneda de la cuenta.
        /// </summary>
        /// <param name="newCurrencyType"></param>
        /// <returns></returns>
        public void ChangeCurrencyType(Currency newCurrencyType)
        {
            CurrencyExchanger currencyExchanger = CurrencyExchanger.Instance;
            if (currencyExchanger.ExistsCurrency(newCurrencyType.Type))
            {
                this.Amount = currencyExchanger.Convert(this.Amount, this.CurrencyType, newCurrencyType);
                this.CurrencyType = newCurrencyType;
            }
        }

        /// <summary>
        /// Cambia el objetivo de ahorro maximo de la cuenta.
        /// </summary>
        /// <param name="objetiveAmount"></param>
        /// <param name="currency"></param>
        /// <param name="timeLimit"></param>
        /// <returns></returns>
        public void ChangeMaxGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
        {
            SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
            this.MaxGoal = newGoal;
        }

        /// <summary>
        /// Cambia el objetivo de ahorro minimo de la cuenta.
        /// </summary>
        /// <param name="objetiveAmount"></param>
        /// <param name="currency"></param>
        /// <param name="timeLimit"></param>
        /// <returns></returns>
        public void ChangeMinGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
        {
            SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
            this.MinGoal = newGoal;
        }
    }
}
