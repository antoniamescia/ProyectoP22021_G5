using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Library
{
    public class TelegramBot : IComunicationChannel
    {
        
        private const string TELEBRAM_BOT_TOKEN = "1871185609:AAGlnk0lPpi-ijJZFgsS_jyUIdVDlSHggzw";
        private static TelegramBot instance;
        private ITelegramBotClient bot;
        
        private TelegramBot()
        {
            this.bot = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }
        public ITelegramBotClient Client
        {
            get
            {
                return this.bot;
            }
        }
        
        private User BotInfo
        {
            get
            {
                return this.Client.GetMeAsync().Result;
            }
        }

        public int BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

        public static TelegramBot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TelegramBot();
                }
                return instance;
            }
        }
        
        
        public void StartCommunication()
        {

        }
        public void ManageMessage(UserMessage message)
        {

        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {

        }
        
        public void SendMessage(UserMessage message)
        {

        }
    }
}
