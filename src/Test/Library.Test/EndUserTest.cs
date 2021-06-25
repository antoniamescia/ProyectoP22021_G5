using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Library.Test
{
    public class EndUserTest
    {
        private EndUser facundo;
        private Account accounts;
        private Currency currency;

        [SetUp]
        public void Setup()
        {
            facundo = new EndUser("Facundo", "1234");
            currency = new Currency("UYU", 1);
            facundo.AddAccount("BBVA", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));
        }

        [Test]
        public void Test1()
        {
            int cant = facundo.Accounts.Count;

            facundo.AddAccount("ITAU", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));
            
            Assert.AreNotEqual(cant, facundo.Accounts.Count);
        }

        [Test]
        public void test2()
        {
            string name = facundo.Username;

            facundo.ChangeUsername("Felipe");

            Assert.AreNotEqual(name, facundo.Username);

        }

        [Test]
        public void test3()
        {
            string pass = facundo.Password;

            facundo.ChangePassword("4321");

            Assert.AreNotEqual(pass, facundo.Password);
        }

        [Test]
        public void test4()
        {   
            int cant = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("Comida");

            Assert.AreNotEqual(cant, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void test5()
        {
            int cant = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("Comida");
            facundo.RemoveExpenseCategory("Comida");

            Assert.AreEqual(cant, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void test6()
        {
            facundo.AddExpenseCategory("Ropa");
            facundo.AddExpenseCategory("Comida");

            facundo.AddExpenseCategory("Ropa");

            Assert.AreEqual(2, facundo.ExpenseCategories.Count);
        }
    }
}