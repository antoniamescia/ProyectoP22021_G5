using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class AccountTest
    {
        private Currency currencyInvented;
        private Account santander;
        private Type type;
        private CurrencyExchanger currencyExchanger;


        [SetUp]
        public void Setup()
        {
            this.currencyExchanger = CurrencyExchanger.Instance;
            this.currencyInvented = new Currency("FelipeCoin", "F$", 100);
            this.type = new Type();
            this.santander = new Account("Santander", type, currencyExchanger.CurrencyList[0], 300, new SavingsGoal(800, 200));
        }

        [Test]
        public void TestTransferIsAddedToTransactionRecordAndAmountChanges() //Prueba que al realizar una transferencia que se agrega a transactionRecord y luego se modifica la cantidad de plata en la cuenta
        {
            double initailAmount = santander.Amount;
            int initialTransactionQuantity = santander.TransactionsRecord.Count;

            santander.Transfer(currencyExchanger.CurrencyList[0], 100, "comida");

            Assert.AreNotEqual(initailAmount, santander.Amount);
            Assert.AreNotEqual(initialTransactionQuantity, santander.TransactionsRecord.Count);
        }

        [Test]
        public void TestChangeCurrencyTypeModifiesAmountAndCurrencyTypeAsExpected() //Prueba que ChangeCurrencyType modifica amount y currencyType, y que sea de la manera deseada 
        {
            Currency initialCurrency = santander.CurrencyType;
            double initialAmount = santander.Amount;
            santander.ChangeCurrencyType(currencyExchanger.CurrencyList[1]);

            double expectedAmount = 300 / 43.9;

            Assert.IsTrue(initialAmount != santander.Amount && santander.Amount == expectedAmount);
            Assert.IsTrue(initialCurrency != santander.CurrencyType && santander.CurrencyType == currencyExchanger.CurrencyList[1]);
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

        [Test]
        public void TestTransferAutoConvertTypeOfCurrency() //Prueba que el metodo transfer aplica el cambio de moneda si se añade dolares a una cuenta de pesos(por ejemplo)
        {
            santander.Transfer(currencyExchanger.CurrencyList[1], 5, "comida");

            double convert = 300 + (5 * 43.9);
            
            Assert.AreEqual(santander.Amount, convert);

        }

        [Test]
        public void TestDisplayAccountType() //Prueba que funciona correctamente
        {
            string expectedResult = "1 - Debito\n2 - Credito\n";
            
            Assert.AreEqual(expectedResult, Account.DisplayAccountType());
        }
    }
}