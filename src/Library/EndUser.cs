using System;
using System.Collections.Generic;

namespace Library
{
    public class EndUser
    {
        //La clase User cumple con el patrón Expert pues es el experto en la información sobre el usuario.
        //Cumple con SRP pues no se encuentra más de una razón de cambio para la clase. 
        //Crea instancias de Account porque las usa de manera muy estrecha, por lo que cumple con el patrón Creator. 
        

        private List<Account> accounts;
        public EndUser(string username, string password)
        {

        }

        public string Username
        {
            get;
            private set;
        }
        public string Password
        {
            get;
            private set;
        }
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Account> Accounts
        {
            get
            {
                return accounts.AsReadOnly();
            }
        }
        //faltaria el set si es necesario de la lista de Accounts
        
        public void ChangeUsername(string newUsername)
        {

        }
        public void ChangePassword(string newPassword)
        {

        }
        public string DisplayAccounts()
        {
            return null;
        }
        public Account AddAccount(string name, Currency currencyType)
        {
            return null;
        }
        public void RemoveAccount()
        {

        }
        public string DisplayExpenseCategories()
        {
            return null;
        }
        public void AddExpenseCategory()
        {

        }
        public void RemoveExpenseCategory()
        {

        }
    }
}
