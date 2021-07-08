// using System;
// using System.Collections.Generic;

// namespace Library
// {
//     public class Account
//     {

//         /*
//         Patrones y principios:
//         Cumple con SRP porque no se identifica más de un razón de cambio.
//         Cumple con el patrón Expert pues es el experto en la información requerida para realizar las responsabilidades otorgadas. 
//         Cumple con el patrón Creator al crear las transacciones pues usa de forma directa dichas instancias al ser el encargado de realizar las transacciones.
//         */
    

//         private List<Transaction> transactionsRecord;
//         private CurrencyExchanger currencyExchanger;
//         public string Name { get; set; }
//         public Currency CurrencyType { get; private set; }
//         public double Amount { get; private set; }
//         public SavingsGoal MaxGoal { get; private set; }
//         public SavingsGoal MinGoal { get; private set; }

//         public Account(string name, Currency currencyType, double amount, SavingsGoal maxGoal, SavingsGoal minGoal)
//         {
//             this.transactionsRecord = new List<Transaction>();
//             this.currencyExchanger = CurrencyExchanger.Instance;
//             this.Name = name;
//             this.CurrencyType = currencyType;
//             this.Amount = amount;
//             this.MaxGoal = maxGoal;
//             this.MinGoal = minGoal;

//         }

//         public IList<Transaction> TransactionsRecord
//         {
//             get
//             {
//                 return transactionsRecord.AsReadOnly();
//             }
//         }
//         /// <summary>
//         /// Realiza una transacción ya sea de ingreso o egreso.
//         /// </summary>
//         /// <param name="currency"></param>
//         /// <param name="amount"></param>
//         /// <param name="description"></param>
//         /// <returns></returns>
//         public void Transfer(Currency currency, double amount, string description)
//         {
//             Transaction transaction = new Transaction(amount, description, currency, DateTime.Now);
//             this.transactionsRecord.Add(transaction);
//             this.Amount += this.currencyExchanger.Convert(amount, currency, this.CurrencyType);
//         }

//         /// <summary>
//         /// Cambia el tipo de moneda de la cuenta.
//         /// </summary>
//         /// <param name="newCurrencyType"></param>
//         /// <returns></returns>
//         public void ChangeCurrencyType(Currency newCurrencyType)
//         {
//             if (currencyExchanger.ExistsCurrency(newCurrencyType.Type))
//             {
//                 this.Amount = this.currencyExchanger.Convert(this.Amount, this.CurrencyType, newCurrencyType);
//                 this.CurrencyType = newCurrencyType;
//             }
//         }

//         /// <summary>
//         /// Cambia el objetivo de ahorro maximo de la cuenta.
//         /// </summary>
//         /// <param name="objetiveAmount"></param>
//         /// <param name="currency"></param>
//         /// <param name="timeLimit"></param>
//         /// <returns></returns>
//         public void ChangeMaxGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
//         {
//             SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
//             this.MaxGoal = newGoal;
//         }

//         /// <summary>
//         /// Cambia el objetivo de ahorro minimo de la cuenta.
//         /// </summary>
//         /// <param name="objetiveAmount"></param>
//         /// <param name="currency"></param>
//         /// <param name="timeLimit"></param>
//         /// <returns></returns>
//         public void ChangeMinGoal(double objectiveAmount, Currency currency, DateTime timeLimit)
//         {
//             SavingsGoal newGoal = new SavingsGoal(objectiveAmount, currency, timeLimit);
//             this.MinGoal = newGoal;
//         }
//     }
// }

using System;
using System.Text;
using System.Collections.Generic;

namespace Bankbot
{
    public enum AccountType
    {
        Ahorro = 1,
        Debito = 2,
        Credito = 3
    }
 
    public class Account
    {
        public string Name { get; set; }
        public List<Transaction> History { get; set; }
        public AccountType AccountType { get; set; }
        public Currency Currency { get; set; }
        public double Balance { get; set; }
        public SavingsGoal SavingsGoal { get; set; }

        public Account(string name, AccountType type, Currency currency, double balance, SavingsGoal savingsGoal)
        {
            this.Name = name;
            this.History = new List<Transaction>();
            this.AccountType = type;
            this.Currency = currency;
            this.Balance = balance;
            this.SavingsGoal = savingsGoal;
        }

        /// <summary>
        /// Cambiar el objetivo de una cuenta.
        /// </summary>
        /// <param name="newObjective"></param>

        public void ChangeSavingsGoal(SavingsGoal newSavingsGoal)
        {
            this.SavingsGoal = newSavingsGoal;
        }

        /// <summary>
        /// Añadir una transacción a una cuenta.
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>

        public void AddTransaction(Currency currency, double amount, string description)
        {
            Transaction transaction = new Transaction(amount, currency, DateTime.Now, description);
            this.History.Add(transaction);
            this.Balance += amount;
        }

        public void ChangeObjective(double newMax, double newMin)
        {
            this.SavingsGoal.Max = newMax;
            this.SavingsGoal.Min = newMin;
        }

        public static string ShowAccountType()
        {
            StringBuilder enumToText = new StringBuilder();
            var accountType = Enum.GetNames(typeof(AccountType));
            foreach (var item in accountType)
            {
                enumToText.Append($"{Array.IndexOf(accountType, item) + 1 } - {item}\n");
            }
            return enumToText.ToString();
        }
    }
}
