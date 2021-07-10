using System.Collections.Generic;

namespace BankerBot
{
    public class Session 
    {
        //SINGLETON
        public Dictionary<string, Data> DataMap;
         public List<EndUser> AllUsers { get; set; }
        //public IPrinter Printer { get; set; }
        private static Session instance;
        public static Session Instance
        {
            get
            {
                if (instance == null) 
                {
                    instance = new Session();
                }
                return instance;
            }
        }
        private Session()
        {
            this.AllUsers = new List<EndUser>();
            this.DataMap = new Dictionary<string, Data>();
            //this.Printer = new HtmlPrinter();
        }


        public void AddUser(string username, string password)
        {
            foreach (EndUser user in AllUsers)
            {
                
                if (user.Username == username)
                {
                    return;
                }
            }
            AllUsers.Add(new EndUser(username, password));
        }

        public bool UsernameExists(string username)
        {
            string u = "";
            foreach (EndUser user in AllUsers)
            {
                if (user.Username == username)
                {
                    u = user.Username;
                }
            }
            return u == username;
        }
        public EndUser GetUser(string username, string password)
        {
            foreach (EndUser user in AllUsers)
            {
                if (user.Username == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

         public void SetChannel(string id, ICommunicationChannel newChannel)
        {
            GetChat(id).Channel = newChannel;
        }

        public Data GetChat(string id)
        {
            Data newChat;
            if (DataMap.TryGetValue(id, out newChat))
            {
                return newChat;
            }

            newChat = new Data();
            DataMap.Add(id, newChat);
            return newChat;

        }
    }
}