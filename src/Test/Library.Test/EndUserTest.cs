using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class EndUserTest
    {
        private EndUser facundo;
        private Currency currency;
        private Type type;

        [SetUp]
        public void Setup()
        {
            this.facundo = new EndUser("Facundo", "1234");
            this.currency = new Currency("UYU", "UY$", 1);
            this.type = new Type ();
            facundo.AddAccount(type, "BBVA", currency, 300, new SavingsGoal(800, 200));
        }

        [Test]
        public void TestAccountIsAddedAndThenRemoved() //Prueba que se agrega una cuenta al usuario y luego se saca
        {
            int initialQuantity = facundo.Accounts.Count;

            facundo.AddAccount(type, "ITAU", currency, 300, new SavingsGoal(800, 200));

            int secondQuantity = facundo.Accounts.Count;

            facundo.RemoveAcount(facundo.Accounts[1]);
            
            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.Accounts.Count);
        }

        [Test]
        public void TestAccountCantBeRepeatedByName() //Prueba que no permite agregar una nueva cuenta con un nombre ya existente
        {
            int cant = facundo.Accounts.Count;

            facundo.AddAccount(type, "BBVA", currency, 400, new SavingsGoal(900, 200));
            
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

            facundo.AddExpenseCategory("Rifa");

            int secondQuantity = facundo.ExpenseCategories.Count;

            facundo.RemoveExpenseCategory("Rifa");

            Assert.AreNotEqual(initialQuantity, secondQuantity);
            Assert.AreNotEqual(secondQuantity, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void TestCantAddRepeatedExpenseCategory() //Prueba que no se puede agregar categorias repetidas, sin importar si esta en mayuscula o minuscula
        {
            facundo.AddExpenseCategory("Rifa");
            facundo.AddExpenseCategory("Salud");

            int expectedQuantity = facundo.ExpenseCategories.Count;

            facundo.AddExpenseCategory("rifa");
            facundo.AddExpenseCategory("RIFA");

            Assert.AreEqual(expectedQuantity, facundo.ExpenseCategories.Count);
        }

        [Test]
        public void TestDisplayAccountsReturnsTextAsExpected() //Prueba que DisplayAccounts devuelve el texto esperado
        {
            facundo.AddAccount(type, "ITAU", currency, 300, new SavingsGoal(800, 200));
            string expectedResult = "1 - BBVA\n2 - ITAU\n";

            Assert.AreEqual(expectedResult, facundo.DisplayAccounts());
        }

        [Test]
        public void TestDisplayExpenseCategoriesReturnsTextAsExpected() //Prueba que DisplayExpenseCategories devuelve el texto esperado
        {
            facundo.AddExpenseCategory("Rifa");
            facundo.AddExpenseCategory("Salud");

            string expectedResult = "1 - Comida\n2 - Ropa\n3 - Alquiler\n4 - Pagos fijos\n5 - Tarjetas\n6 - Luz\n7 - Transporte\n8 - Agua\n9 - Mascota\n10 - Regalos\n11 - Diversión\n12 - Rifa\n13 - Salud\n"; 

            Assert.AreEqual(expectedResult, facundo.DisplayExpenseCategories());
        }

        [Test]
        public void TestIfTheAccountExistReturnTrueElseReturnsFalse() //Prueba que el metodo AccountExists devuelve true si la cuenta existe o false en caso contrario
        {
            Assert.IsTrue(facundo.AccountExists("BBVA"));
            Assert.IsFalse(facundo.AccountExists("Santander"));
        }

        [Test]
        public void TestIfTheCategoryExistReturnTrueElseReturnsFalse() //Prueba que el metodo AccountExists devuelve true si la cuenta existe o false en caso contrario
        {
            Assert.IsTrue(facundo.ContainsExpenseCategory("Comida"));
            Assert.IsFalse(facundo.ContainsExpenseCategory("CategoriaFalsa"));
        }

        [Test]
        public void TestContainsExpenseCategoryIsntCaseSensitive() //Prueba que el metodo AccountExists devuelve true si la cuenta existe o false en caso contrario
        {
            Assert.AreEqual(facundo.ContainsExpenseCategory("COMIDA"), facundo.ContainsExpenseCategory("comida"));
        }
    }
}