using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Library
{
    public class TelegramBot : IComunicationChannel
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con LSP porque el tipo implícito que define la clase puede ser sustiuido por ICommunicationChannel.
        Cumple con ISP porque solo implementa una interfaz (ICommunicationChannel).
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con las responsabilidades otorgadas.
        Cumple con Polymorphism porque usa los métodos polimórfico StatCommunication, ManageMessage y SendMessage.
        */
        
        private const string TELEBRAM_BOT_TOKEN = "1871185609:AAGlnk0lPpi-ijJZFgsS_jyUIdVDlSHggzw";
        private ITelegramBotClient bot;
        private static TelegramBot instance;
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
        
        
        public void StartCommunication()
        {
            bot.OnMessage += OnMessage;
            bot.StartReceiving();
        }
        public void ManageMessage(UserMessage message)
        {

        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            // Message message = messageEventArgs.Message;
            // string chatId = message.Chat.Id.ToString();

            // UserMessage msg = new UserMessage(chatId, message.Text);
            // SetChannel(chatId, this);
            // TelegramBot.Instance.HandleMessage(msg);

        }
        
        public void SendMessage(UserMessage message)
        {

        }
    }
}
