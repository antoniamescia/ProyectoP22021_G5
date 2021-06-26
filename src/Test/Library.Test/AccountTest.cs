using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Library.Test
{
    public class AccountTest
    {
        private Currency currencyPesos;
        private Currency currencyDollars;
        private Account santander;

        [SetUp]
        public void Setup()
        {
            this.currencyPesos = new Currency("UYU", 1);
            this.currencyDollars = new Currency("USD", 45.05);
            this.santander = new Account("Santander", currencyPesos, 300, new SavingsGoal(800, currencyPesos, new DateTime(2021,06,25)), new SavingsGoal(200, currencyPesos, new DateTime(2021,06,25)));
        }

        [Test]
        public void Test1() //
        {
            double initailAmount = santander.Amount;
            int initialTransactionQuantity = santander.TransactionsRecord.Count;

            santander.Transfer(currency)

            Assert.AreNotEqual()
        }
    }
}