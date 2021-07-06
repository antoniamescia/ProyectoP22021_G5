using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class AccountTest
    {
        private Currency currencyPesos;
        private Currency currencyDollars;
        private Currency currencyInvented;
        private Account santander;

        [SetUp]
        public void Setup()
        {
            this.currencyPesos = new Currency("UYU", 1);
            this.currencyDollars = new Currency("USD", 45.05);
            this.currencyInvented = new Currency("FelipeCoin", 100);
            this.santander = new Account("Santander", currencyPesos, 300, new SavingsGoal(800, currencyPesos, new DateTime(2021,06,25)), new SavingsGoal(200, currencyPesos, new DateTime(2021,06,25)));
        }

        [Test]
        public void TestTransferIsAddedToTransactionRecordAndAmountChanges() //Prueba que al realizar una transferencia que se agrega a transactionRecord y luego se modifica la cantidad de plata en la cuenta
        {
            double initailAmount = santander.Amount;
            int initialTransactionQuantity = santander.TransactionsRecord.Count;

            santander.Transfer(currencyPesos, 100, "comida");

            Assert.AreNotEqual(initailAmount, santander.Amount);
            Assert.AreNotEqual(initialTransactionQuantity, santander.TransactionsRecord.Count);
        }

        [Test]
        public void TestChangeCurrencyTypeModifiesAmountAndCurrencyTypeAsExpected() //Prueba que ChangeCurrencyType modifica amount y currencyType, y que sea de la manera deseada 
        {
            Currency initialCurrency = santander.CurrencyType;
            double initialAmount = santander.Amount;
            santander.ChangeCurrencyType(currencyDollars);
 
            double expectedAmount = 300 / 45.05;

            Assert.IsTrue(initialAmount != santander.Amount && santander.Amount == expectedAmount);
            Assert.IsTrue(initialCurrency != santander.CurrencyType && santander.CurrencyType == this.currencyDollars);
        }

        [Test]
        public void TestCantChangeToACurrencyThatDoesntExist() //Prueba que no permite cambiar la moneda a una inventada que no este en el sistema 
        {
            Currency initialCurrency = santander.CurrencyType;
            double initialAmount = santander.Amount;
            santander.ChangeCurrencyType(currencyInvented);
 
            Assert.AreEqual(initialAmount, santander.Amount);
            Assert.AreEqual(initialCurrency, santander.CurrencyType);
        }
    }
}