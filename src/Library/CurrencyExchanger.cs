using System;
using System.Collections.Generic;

namespace Library
{
    public class CurrencyExchanger
    {
        private List<Currency> currencyList;
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Currency> CurrencyList
        {
            get
            {
                return currencyList.AsReadOnly();
            }
        }
        //faltaria el set de la lista de Currency si es necesario
        
        public void AddCurrency(Currency curency)
        {

        }
        public void RemoveCurrency(Currency currency)
        {

        }
        
        public double Convert(double amount, Currency initialCurrency, Currency finalCurrency)
        {
            return 0;
        }
    }
}
