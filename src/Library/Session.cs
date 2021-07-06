using System.Collections.Generic;

namespace Library
{
    class Session
    {
        public List<EndUser> AllEndUsers { get; set; }
        public Dictionary<string, UserInfo> UserInfoMap;
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
            this.AllEndUsers = new List<EndUser>();
            this.UserInfoMap = new Dictionary<string, UserInfo>();
            //PRINTER

        }

        public void AddUser(string username, string password)
        {
            foreach (var user in AllEndUsers)
            {
                if (user.Username == username) return;
            }
            AllEndUsers.Add(new EndUser(username, password));
        }

        public EndUser GetEndUser(string username, string password)
        {
            foreach (EndUser endUser in AllEndUsers)
            {
                if (endUser.Username == username && endUser.Password == password)
                {
                    return endUser;
                }
            }
            return null;
        }

        public bool UsernameExists(string username)
        {
            string user = "";
            foreach (var item in AllEndUsers)
            {
                if (item.Username == username) user = item.Username;
            }
            return user == username;
        }

        public UserInfo GetChatInfo(string id)
        {
            UserInfo chat = new UserInfo();
            if (UserInfoMap.TryGetValue(id, out chat))
            {
                return chat;
            }
            UserInfoMap.Add(id, chat);
            return chat;
        }

        public void SetComunicationChannel(string id, IComunicationChannel newComunicationChannel)
        {
            GetChatInfo(id).ComunicationChannel = newComunicationChannel;
        }

    }
}