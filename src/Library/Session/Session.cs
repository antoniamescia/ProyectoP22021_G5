using System.Collections.Generic;

namespace BankerBot
{
    public class Session 
    {
        public Dictionary<string, UserInfo> UserInfoMap;
        public List<EndUser> AllUsers { get; set; }
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

        /// <summary>
        /// Constructor de Session
        /// </summary>
        private Session()
        {
            this.AllUsers = new List<EndUser>();
            this.UserInfoMap = new Dictionary<string, UserInfo>();
            this.Printer = new HTMLPrinter();
        }

        /// <summary>
        /// Agrega un usuario nuevo en caso que no exista
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
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

        /// <summary>
        /// Se fija si ya existe ese nombre de usuario
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Se fija si existe ese usuario con su respectiva contraseña, y si no no devuelve nada
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Setea el canal de comunicacion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newChannel"></param>
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