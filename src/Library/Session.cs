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