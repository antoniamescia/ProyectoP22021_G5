﻿using System;
using System.Collections.Generic;

namespace Library
{
    public class Account
    {
       
    //Cumple con el patrón Expert pues es el experto en la información requerida para realizar las responsabilidades otorgadas. 
    //Cumple con el patrón Creator al crear las transacciones pues usa de forma directa dichas instancias al ser el encargado de realizar las transacciones.
    // A su vez cumple con el patrón OCP, ya que es una clase que se encuentra abierta a la extensión, pero cerrada a la modificación.

        private List<Transaction> transactionsRecord;
        public Account (string name, Currency currencyType)
        {
            this.Name = name;
            this.CurrencyType = currencyType;
        }

        public string Name
        {
            get;
            set;
        }
        public Currency CurrencyType
        {
            get;
            private set;
        }
        public int Amount
        {
            get;
            private set;
        }
        //puse que los get devuelvan una IList<T> para poder devolver la lista de modo AsReadOnly
        public IList<Transaction> TransactionsRecord
        {
            get
            {
                return transactionsRecord.AsReadOnly();
            }
        }

        public void Transfer(Currency currency, double ammdeount, string description)
        {
            Transaction transaction = new Transaction();
        }
        public void ChangeCurrencyType(Currency newCurrencyType)
        {

        }
    }
}