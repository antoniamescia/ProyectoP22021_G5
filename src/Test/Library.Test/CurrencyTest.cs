using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class CurrencyTests
    {
        private CurrencyExchanger currencyExchanger = CurrencyExchanger.Instance;
    

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CreateCurrency() //Prueba que se agrega una nueva divisa
        {
        
           currencyExchanger.AddCurrency("ARG", "ARG$", 0.5);
           Assert.IsTrue(currencyExchanger.ExistsCurrency("ARG$"));
        }

        [Test]
        public void CurrencyExists() //Prueba que no exista la divisa antes de agregarla
        {
            currencyExchanger.AddCurrency("ARG","ARG$", 1);
            int count = 0;
            foreach (Currency currency in currencyExchanger.CurrencyList)
            {
                if (currency.Type == "ARG") count++;
            }
            Assert.IsFalse(count == 2);
        }    

    }

}
