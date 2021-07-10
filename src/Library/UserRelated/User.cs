﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BankerBot
{
    public class User
    {
        /*
        Patrones y principos:
        Cumple con el patrón Expert pues es el experto en la información sobre el usuario.
        Cumple con SRP pues no se encuentra más de una razón de cambio para la clase. 
        Crea instancias de Account porque las usa de manera muy estrecha, por lo que cumple con el patrón Creator. 
        */
        public List<Account> Accounts { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<String> ExpenseCategories { get; set; }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Accounts = new List<Account> { };
            this.ExpenseCategories = new List<String> { "Comida", 
                                                        "Ropa", 
                                                        "Alquiler", 
                                                        "Pagos fijos", 
                                                        "Tarjetas", 
                                                        "Luz", 
                                                        "Transporte", 
                                                        "Agua", 
                                                        "Mascota", 
                                                        "Regalos", 
                                                        "Diversión" };
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
            StringBuilder accountList = new StringBuilder();
            foreach (Account account in Accounts)
            {
                string index = (Accounts.IndexOf(account) + 1).ToString();
                accountList.Append(index + " - " + account.Name + "\n");
            }
            return accountList.ToString();
        }

        public Account AddAccount(Type type, string name, Currency currency, double balance, SavingsGoal savingsGoal)
        {
            if (this.Accounts == null)
            {
                this.Accounts = new List<Account> { };
            }
            foreach (Account account in Accounts)
            {
                if (account.Name == name)
                {
                    return null;
                }
            }
            Account newAccount = new Account(name, type, currency, balance, savingsGoal);
            this.Accounts.Add(newAccount);
            return newAccount;
        }

        public bool AccountExists(string name)
        {
            string accountName = String.Empty;

            foreach (Account account in Accounts)
            {
                if (account.Name == name)
                {
                    accountName = account.Name;
                }
            }

            return accountName == name;
        }

        public void RemoveAcount(Account account)
        {
             if (this.Accounts.Contains(account))
            {
                this.Accounts.Remove(account);
            }
            else
            {
                Console.WriteLine("¡Disculpas! No hemos encontrado la cuenta que quieres remover. ");
            }
        }

        public string DisplayExpenseCategories()
        {
            StringBuilder categoriesList = new StringBuilder();
            foreach (string category in ExpenseCategories)
            {
                string index = (ExpenseCategories.IndexOf(category) + 1).ToString();
                categoriesList.Append(index + " - " + category + "\n");
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

        public bool ContainsExpenseCategory(string newExpenseCategory)
        {
            string exists = string.Empty;
            foreach (string item in ExpenseCategories)
            {
                if (item.ToLower() == newExpenseCategory.ToLower())
                {
                    exists = item;
                }
            }
            return exists == newExpenseCategory;
        }

        public bool Login(string password)
        {
            return true;
        }


    }
}