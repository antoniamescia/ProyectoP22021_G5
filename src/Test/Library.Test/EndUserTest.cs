using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Library.Test
{
    public class EndUserTest
    {
        private EndUser facundo;
        private Currency currency;

        [SetUp]
        public void Setup()
        {
            facundo = new EndUser("Facundo", "1234");
            currency = new Currency("UYU", 1);
            facundo.AddAccount("BBVA", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));
        }

        [Test]
        public void TestAccountIsAddedAndThenRemoved() //addaccount
        {
            int initialQuantity = facundo.Accounts.Count;

            facundo.AddAccount("ITAU", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));

            int secondQuantity = facundo.Accounts.Count;

            facundo.RemoveAccount(facundo.Accounts[1]);
            
            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.Accounts.Count);
        }

        [Test]
        public void TestAccountCantBeRepeatedByName() //addaccount
        {
            int cant = facundo.Accounts.Count;

            facundo.AddAccount("BBVA", currency, 400, new SavingsGoal(900, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));
            
            Assert.AreEqual(cant, facundo.Accounts.Count);
        }


        // [Test]  
        // public void testx1() 
        // {
        //     int  cant = facundo.Accounts.Count;

        //     facundo.RemoveAccount(facundo.Accounts[3]);

        //     Assert.AreEqual(cant, facundo.Accounts.Count);
        // }

        [Test]
        public void TestUsernameChanges() //username
        {
            string name = facundo.Username;

            facundo.ChangeUsername("Felipe");

            Assert.AreNotEqual(name, facundo.Username);

        }

        [Test]  //pass
        public void TestPasswordChanges()
        {
            string pass = facundo.Password;

            facundo.ChangePassword("4321");

            Assert.AreNotEqual(pass, facundo.Password);
        }

        [Test]
        public void TestCategoryIsAddedAndThenRemoved() //removecategory
        {
            int initialQuantity = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("Comida");
            int secondQuantity = facundo.ExpenseCategories.Count;
            facundo.RemoveExpenseCategory("Comida");

            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void test6() //addcategory
        {
            facundo.AddExpenseCategory("Ropa");
            facundo.AddExpenseCategory("Comida");

            facundo.AddExpenseCategory("Ropa");

            Assert.AreEqual(2, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void test45() //removecategory
        {
            int cant = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("Comida");
            facundo.RemoveExpenseCategory("Ropa");

            Assert.AreNotEqual(cant, facundo.ExpenseCategories.Count);
        }
    }
}