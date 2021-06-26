using System;
using System.Text;
using System.Collections.Generic;

namespace Library
{
    public class EndUser
    {
        //La clase User cumple con el patrón Expert pues es el experto en la información sobre el usuario.
        //Cumple con SRP pues no se encuentra más de una razón de cambio para la clase. 
        //Crea instancias de Account porque las usa de manera muy estrecha, por lo que cumple con el patrón Creator. 
        //Tambien cumple con el patrón OCP al ser una clase abierta a la extensión y cerrada a la modificación.

        private List<Account> accounts;
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<string> ExpenseCategories { get; set; }

        public EndUser(string username, string password)
        {
            this.accounts = new List<Account> { };
            this.Username = username;
            this.Password = password;
            this.ExpenseCategories = new List<String> {/*agregar  categorias de gastos*/ };
        }


        public IList<Account> Accounts
        {
            get
            {
                return accounts.AsReadOnly();
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
        public string DisplayAccounts()
        {
            StringBuilder accountsList = new StringBuilder();
            foreach (Account account in Accounts)
            {
                accountsList.Append(account.Name + "\n");
            }
            return accountsList.ToString();
        }
        public Account AddAccount(string name, Currency currencyType, double amount, SavingsGoal maxGoal, SavingsGoal minGoal)
        {
            foreach (Account account in Accounts)
            {
                if (account.Name == name)
                {
                    return null;
                }
            }
            Account newAccount = new Account(name, currencyType, amount, maxGoal, minGoal);
            this.accounts.Add(newAccount);
            return newAccount;
        }
        public void RemoveAccount(Account account)
        {
            if (this.Accounts.Contains(account))
            {
                this.Accounts.Remove(account);
            }
            else
            {
                Console.WriteLine("No se ha encontrado la cuenta: " + account.Name);
            }
        }
        public string DisplayExpenseCategories()
        {
            StringBuilder categoriesList = new StringBuilder();
            foreach (string category in ExpenseCategories)
            {
                categoriesList.Append(category + "\n");
            }
            return categoriesList.ToString();

        }
        public void AddExpenseCategory(string newCategory)
        {
            bool containsCategory = false;
            foreach (string category in ExpenseCategories)
            {
                if (category.ToLower() == newCategory.ToLower())
                {
                    containsCategory = true;
                    break;
                }
            }
            if (!containsCategory)
            {
                ExpenseCategories.Add(newCategory);
            }
        }
        public void RemoveExpenseCategory(string categoryToRemove)
        {
            foreach (string category in ExpenseCategories)
            {
                if (category.ToLower() == categoryToRemove.ToLower())
                {
                    ExpenseCategories.Remove(category);
                    break;
                }
            }
        }
    }
}
