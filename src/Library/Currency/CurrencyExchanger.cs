using System.Collections.Generic;
using System.Text;

namespace BankerBot
{
    public class CurrencyExchanger
    {
        public List<Currency> CurrencyList { get; set; }
        private static CurrencyExchanger instance;
        public static CurrencyExchanger Instance
        {
            get
            {
                if (instance == null) instance = new CurrencyExchanger();
                return instance;
            }
        }

        private CurrencyExchanger()
        {
            this.CurrencyList = new List<Currency>() { new Currency("UYU", "U$", 1), new Currency("USS", "US$", 43.9), new Currency("EUR", "€ ", 55), new Currency("BRL", "R$", 8.36) };
        }
      

        public void AddCurrency(string code, string type, double rate)
        {
            if (!CurrencyExists(type))
            {
                Currency newCurrency = new Currency(code, type, rate);
                CurrencyList.Add(newCurrency);
            }
        }
        public void RemoveCurrency(string type)
        {
            foreach (Currency currency in CurrencyList)
            {
                if (currency.Type == type)
                {
                    CurrencyList.Remove(currency);
                    return;
                }
            }
        }

        public double Convert(double amount, Currency from, Currency to)
        {
            if (from.Code != "UYU")
            {
                amount = amount * from.ConvertionRate;
            }

            return amount / to.ConvertionRate;
        }

        public bool CurrencyExists(string type)
        {
            foreach (Currency currency in CurrencyList)
            {
                if (currency.Type == type) return true;
            }
            return false;
        }

        public string DisplayCurrencyList()
        {
            StringBuilder currencies = new StringBuilder();
            foreach (Currency currency in CurrencyExchanger.Instance.CurrencyList)
            {
                currencies.Append($"{CurrencyExchanger.Instance.CurrencyList.IndexOf(currency) + 1} - {currency.Code}\n");
            }
            return currencies.ToString();
        }
    }
}
