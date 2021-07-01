using NUnit.Framework;
using System.Collections.Generic;

namespace BankerBot.Test
{
    public class CurrencyTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateCurrency()
        {
           CurrencyExchanger.Instance.AddCurrency("type", 1);
           Assert.IsTrue(CurrencyExchanger.Instance.ExistsCurrency("type"));
        }

        [Test]
        public void CurrencyExists()
        {
            CurrencyExchanger.Instance.AddCurrency("type", 1);
            int count = 0;
            foreach (var item in CurrencyExchanger.Instance.CurrencyList)
            {
                if (item.Type == "type") count++;
            }
            Assert.IsFalse(count == 2);
        }    
    }

}
