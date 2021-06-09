using System;
using System.Collections.Generic;

namespace Library
{
    public class User
    {
        private List<Account> accounts;
        public User(string username, string password)
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
        public void AddAccount()
        {

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
