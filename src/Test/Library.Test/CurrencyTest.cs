using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class CurrencyTests
    {
        private CurrencyExchanger currencyExchanger;
    

        [SetUp]
        public void Setup()
        {
            this.currencyExchanger = CurrencyExchanger.Instance;
        }

        [Test]
        public void TestCreateNewCurrencyAndDisplaysItCorrectly() //Prueba que se agrega una nueva divisa y la muestra correctamente
        {

            currencyExchanger.AddCurrency("ARG", "ARG$", 0.5);
            string expectedString = "1 - UYU\n2 - USD\n3 - EUR\n4 - BRL\n5 - ARG\n";

            Assert.AreEqual(expectedString, currencyExchanger.DisplayCurrencyList());
            Assert.IsTrue(currencyExchanger.ExistsCurrency("ARG$"));
        }

        [Test]
        public void TestCantAddCurrencyTwice() //Prueba que no exista la divisa antes de agregarla
        {
            currencyExchanger.AddCurrency("ARG","ARG$", 1);
            int expectedQuantity = currencyExchanger.CurrencyList.Count;
            currencyExchanger.AddCurrency("ARG","ARG$", 1);

            Assert.AreEqual(expectedQuantity, currencyExchanger.CurrencyList.Count);
        }

        [Test]
        public void TestRightCurrencyIsRemoved() //Prueba que se elimina la divisa correcta
        {
            int initialQuantity = currencyExchanger.CurrencyList.Count;
            currencyExchanger.RemoveCurrency("R$");
            
            Assert.AreNotEqual(initialQuantity, currencyExchanger.CurrencyList.Count);
            Assert.IsFalse(currencyExchanger.ExistsCurrency("R$"));
        }

        [Test]
        public void TestExistCurrencyReturnsTheCorrectAnswer() //Prueba que ExistCurrency devuelve true si existe la moneda en el sistema y false si no extiste en el sistema
        {
            Assert.IsTrue(currencyExchanger.ExistsCurrency("R$"));
            Assert.IsFalse(currencyExchanger.ExistsCurrency("InventedCurrency"));
        }

    }

}
