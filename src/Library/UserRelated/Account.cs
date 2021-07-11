using System;
using System.Text;
using System.Collections.Generic;

namespace BankerBot
{

    /// <summary>
    /// 
    /// </summary>
    public enum Type
    {
        Debito = 1,
        Credito = 2


    }

    public class Account
    {

        /*
        Patrones y principios:
        Cumple con SRP porque no se identifica más de un razón de cambio.
        Cumple con el patrón Expert pues es el experto en la información requerida para realizar las responsabilidades otorgadas. 
        Cumple con el patrón Creator al crear las transacciones pues usa de forma directa dichas instancias al ser el encargado de realizar las transacciones.
        */


        public List<Transaction> TransactionsRecord { get; private set; }
        private CurrencyExchanger currencyExchanger;
        public Type AccountType { get; set; }
        public string Name { get; set; }
        public Currency CurrencyType { get; set; }
        public double Amount { get; set; }
        public SavingsGoal SavingsGoal { get; set; }


        /// <summary>
        /// Constructor de la cuenta
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="savingsGoal"></param>
        public Account(string name, Type type, Currency currency, double amount, SavingsGoal savingsGoal)
        {
            this.TransactionsRecord = new List<Transaction>();
            this.currencyExchanger = CurrencyExchanger.Instance;
            this.AccountType = type;
            this.Name = name;
            this.CurrencyType = currency;
            this.Amount = amount;
            this.SavingsGoal = savingsGoal;
        }

        /// <summary>
        /// Realiza una transacción ya sea de ingreso o egreso.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        public void Transfer(Currency currency, double amount, string description)
        {
            Transaction transaction = new Transaction(amount, currency, DateTime.Now, description);
            this.TransactionsRecord.Add(transaction);
            this.Amount += this.currencyExchanger.Convert(amount, currency, this.CurrencyType);
        }

        /// <summary>
        /// Cambia el tipo de moneda de la cuenta.
        /// </summary>
        /// <param name="newCurrencyType"></param>
        /// <returns></returns>
        public void ChangeCurrencyType(Currency newCurrencyType)
        {
            if (currencyExchanger.ExistsCurrency(newCurrencyType.Type))
            {
                this.Amount = this.currencyExchanger.Convert(this.Amount, this.CurrencyType, newCurrencyType);
                this.CurrencyType = newCurrencyType;
            }
        }

        /// <summary>
        /// Muestra los tipos de cuenta a crear. En este caso, pueden ser solo Débito o Crédito por los definidos en el tipo enumerable. 
        /// </summary>
        /// <returns></returns>
        public static string DisplayAccountType()
        {
            StringBuilder enumerator = new StringBuilder();
            string[] accountType = Enum.GetNames(typeof(Type));
            foreach (string type in accountType)
            {
                enumerator.Append($"{Array.IndexOf(accountType, type) + 1 } - {type}\n");
            }
            return enumerator.ToString();
        }

        /// <summary>
        /// Cambia objetivo maximo y minimo
        /// </summary>
        /// <param name="newMax"></param>
        /// <param name="newMin"></param>
        public void ChangeSavingsGoal(double newMax, double newMin)
        {
            this.SavingsGoal.Max = newMax;
            this.SavingsGoal.Min = newMin;
        }
    }
}
