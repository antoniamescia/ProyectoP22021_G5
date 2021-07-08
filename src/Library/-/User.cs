using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Bankbot
{
    /*Esta clase cumple con los principios GRAPS, ya que es experta en información sobre los usuarios, se encarga de 
    crear instancias de la clase Account para luego almacenarlos. Por esta razón cumple con los patrones Expert
    y Creator dentro de estos principios.
    Por otro lado cumple con el patrón OCP al ser una clase abierta a la extensión y cerrada a la modificación.*/
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }
        public List<String> ExpenseCategories { get; set; }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Accounts = new List<Account> { };
            this.ExpenseCategories = new List<String> { "Comida", "Ropa", "Alquiler", "Pagos fijos", "Tarjetas", "Luz", "Transporte", "Agua", "Mascota", "Regalos", "Diversión" };
        }

        /// <summary>
        /// Agregar un objeto Account a la la lista List<Account>
        /// </summary>
        /// <param name="account"></param>
        public Account AddAccount(AccountType type, string name, Currency currency, double balance, SavingsGoal savingsGoal)
        {
            if (this.Accounts == null)
            {
                this.Accounts = new List<Account> { };
            }
            foreach (var account in Accounts)
            {
                if (account.Name == name)
                {
                    return null;
                }
            }
            var newAccount = new Account(name, type, currency, balance, savingsGoal);
            this.Accounts.Add(newAccount);
            return newAccount;
        }

        public bool AccountNameExists(string name)
        {
            string accountName = String.Empty;

            foreach (var item in Accounts)
            {
                if (item.Name == name) accountName = item.Name;
            }

            return accountName == name;
        }

        /// <summary>
        /// Quita un objeto Account de la lista List<Account>
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAcount(Account account)
        {
            if (this.Accounts.Contains(account))
            {
                this.Accounts.Remove(account);
            }
            else
            {
                System.Console.WriteLine("No se ha encontrado la cuenta " + account.Name);
            }
        }


        public void ChangeUsername(string newUsername)
        {
            this.Username = newUsername;
        }

        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }

        public string ShowAccountList()
        {
            StringBuilder accountList = new StringBuilder();
            foreach (var account in Accounts)
            {
                string index = (Accounts.IndexOf(account) + 1).ToString();
                accountList.Append(index + " - " + account.Name + "\n");
            }
            return accountList.ToString();
        }
        public string ShowItemList()
        {
            StringBuilder categoriesList = new StringBuilder();
            foreach (string category in ExpenseCategories)
            {
                string index = (ExpenseCategories.IndexOf(category) + 1).ToString();
                categoriesList.Append(index + " - " + category + "\n");
            }
            return categoriesList.ToString();
        }

        public bool ContainsItem(string newItem)
        {
            string exists = string.Empty;
            foreach (var item in ExpenseCategories)
            {
                if (item.ToLower() == newItem.ToLower()) exists = item;
            }
            return exists == newItem;
        }

        public bool Login(string password)
        {
            return true;
        }


    }
}