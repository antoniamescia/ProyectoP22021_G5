using System.Collections.Generic;

namespace BankerBot
{
    public class Session 
    {
        //SINGLETON
        public Dictionary<string, UserInfo> UserInfoMap;
         public List<User> AllUsers { get; set; }
        public IPrinter Printer { get; set; }
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
            this.AllUsers = new List<User>();
            this.UserInfoMap = new Dictionary<string, UserInfo>();
            this.Printer = new HTMLPrinter();
        }


        public void AddUser(string username, string password)
        {
            foreach (User user in AllUsers)
            {
                
                if (user.Username == username)
                {
                    return;
                }
            }
            AllUsers.Add(new User(username, password));
        }

        public bool UsernameExists(string username)
        {
            string u = "";
            foreach (User user in AllUsers)
            {
                if (user.Username == username)
                {
                    u = user.Username;
                }
            }
            return u == username;
        }
        public User GetUser(string username, string password)
        {
            foreach (User user in AllUsers)
            {
                if (user.Username == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

         public void SetComunicationChannel(string id, ICommunicationChannel newChannel)
        {
            GetChatInfo(id).ComunicationChannel = newChannel;
        }

        public UserInfo GetChatInfo(string id)
        {
            UserInfo newChat;
            if (UserInfoMap.TryGetValue(id, out newChat))
            {
                return newChat;
            }

            newChat = new UserInfo();
            UserInfoMap.Add(id, newChat);
            return newChat;

        }
    }
}