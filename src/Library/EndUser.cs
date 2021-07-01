using System;
using System.Text;
using System.Collections.Generic;

namespace BankerBot
{
    public class EndUser
    {
        /*
        Patrones y principos:
        Cumple con el patrón Expert pues es el experto en la información sobre el usuario.
        Cumple con SRP pues no se encuentra más de una razón de cambio para la clase. 
        Crea instancias de Account porque las usa de manera muy estrecha, por lo que cumple con el patrón Creator. 
        */
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

        /// <summary>
        /// Cambia el nombre de usuario del usuario.
        /// </summary>
        /// <param name="mewUsername"></param>
        /// <returns></returns>
        public void ChangeUsername(string newUsername)
        {
            this.Username = newUsername;
        }

        /// <summary>
        /// Cambia la contraseña del usuario.
        /// </summary>
        /// <param name="mewPassword"></param>
        /// <returns></returns>
        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }

        /// <summary>
        /// Despliega todas las cuentas disponibles del usuario.
        /// </summary>
        /// <returns></returns>
        public string DisplayAccounts()
        {
            StringBuilder accountsList = new StringBuilder();
            foreach (Account account in Accounts)
            {
                accountsList.Append(account.Name + "\n");
            }
            return accountsList.ToString();
        }
        /// <summary>
        /// Añade una cuenta a la lista de cuentas de usuario, di dicha cuenta ya existe, no crea nada.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="currencyType"></param>
        /// <param name="amount"></param>
        /// <param name="maxGoal"></param>
        /// <param name="minGoal"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remueve una cuenta de la lista de cuentas del usuario, si dicha cuenta no existe, no borra nada.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public void RemoveAccount(Account account)
        {
            if (this.Accounts.Contains(account))
            {
                this.accounts.Remove(account);
            }
            else
            {
                Console.WriteLine("No se ha encontrado la cuenta");
            }
        }

        /// <summary>
        /// Despliega todos los elementos de la lista de categorias disponibles del usuario.
        /// </summary>
        /// <returns></returns>
        public string DisplayExpenseCategories()
        {
            StringBuilder categoriesList = new StringBuilder();
            foreach (string category in ExpenseCategories)
            {
                categoriesList.Append(category + "\n");
            }
            return categoriesList.ToString();

        }

        /// <summary>
        /// Añade una categoria a la lista de categorias del usuario, si dicha categoria ya existe, no la añade.
        /// </summary>
        /// <param name="newCategory"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remueve una categoria que la lista de categorias del usuario, si dicha categoria no esta, no borra nada.
        /// </summary>
        /// <param name="categoryToRemove"></param>
        /// <returns></returns>
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
