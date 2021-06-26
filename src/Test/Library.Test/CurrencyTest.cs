using NUnit.Framework;
using System.Collections.Generic;
using Library;

namespace Library.Test
{
    public class CurrencyTests
    {
        private Currency libra;

        [SetUp]
        public void Setup()
        {
            this.libra = new Currency ("GBP", 65);
        }

        [Test]
        public void CreateCurrency()
        {
           CurrencyExchanger.Instance.AddCurrency("UYU", 65);
           Assert.IsTrue(CurrencyExchanger.Instance.ExistsCurrency("UYU"));
        }
    }
}
