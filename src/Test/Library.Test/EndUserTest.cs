using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
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
        public void TestAccountIsAddedAndThenRemoved() //Prueba que se agrega una cuenta al usuario y luego se saca
        {
            int initialQuantity = facundo.Accounts.Count;

            facundo.AddAccount("ITAU", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));

            int secondQuantity = facundo.Accounts.Count;

            facundo.RemoveAccount(facundo.Accounts[1]);
            
            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.Accounts.Count);
        }

        [Test]
        public void TestAccountCantBeRepeatedByName() //Prueba que no permite agregar una nueva cuenta con un nombre ya existente
        {
            int cant = facundo.Accounts.Count;

            facundo.AddAccount("BBVA", currency, 400, new SavingsGoal(900, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));
            
            Assert.AreEqual(cant, facundo.Accounts.Count);
        }

        [Test]
        public void TestUsernameChanges()       //Prueba que se modifica el usuario
        {
            string name = facundo.Username;

            facundo.ChangeUsername("Felipe");

            Assert.AreNotEqual(name, facundo.Username);

        }

        [Test]
        public void TestPasswordChanges()       //Prueba que se modifica la contraseña
        {
            string pass = facundo.Password;

            facundo.ChangePassword("4321");

            Assert.AreNotEqual(pass, facundo.Password);
        }

        [Test]
        public void TestCategoryIsAddedAndThenRemoved() //Prueba que se agrega una categoria y luego se saca
        {
            int initialQuantity = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("Comida");

            int secondQuantity = facundo.ExpenseCategories.Count;

            facundo.RemoveExpenseCategory("Comida");

            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void TestCantAddRepeatedExpenseCategory() //Prueba que no se puede agregar categorias repetidas, sin importar si esta en mayuscula o minuscula
        {
            facundo.AddExpenseCategory("Ropa");
            facundo.AddExpenseCategory("Comida");

            int expectedQuantity = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("ropa");
            facundo.AddExpenseCategory("ROPA");

            Assert.AreEqual(expectedQuantity, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void TestDisplayAccountsReturnsTextAsExpected() //Prueba que DisplayAccounts devuelve el texto esperado
        {
            facundo.AddAccount("ITAU", currency, 300, new SavingsGoal(800, currency, new DateTime(2021,06,25)), new SavingsGoal(200, currency, new DateTime(2021,06,25)));

            string expectedResult = "BBVA\nITAU\n";

            Assert.AreEqual(expectedResult, facundo.DisplayAccounts());
        }

        [Test]
        public void TestDisplayExpenseCategoriesReturnsTextAsExpected() //Prueba que DisplayExpenseCategories devuelve el texto esperado
        {
            facundo.AddExpenseCategory("Ropa");
            facundo.AddExpenseCategory("Comida");

            string expectedResult = "Ropa\nComida\n";

            Assert.AreEqual(expectedResult, facundo.DisplayExpenseCategories());
        }
    }
}