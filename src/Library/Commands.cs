using System;
using System.Collections.Generic;

namespace Library
{
    public class Commands
    {
        public List<string> CommandsList { get; set; }
        private static Commands instance;
        public static Commands Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Commands();

                }
                return instance;
            }
        }
        private Commands()
        {
            this.CommandsList = new List<string>() {};
            
        }
    }
}