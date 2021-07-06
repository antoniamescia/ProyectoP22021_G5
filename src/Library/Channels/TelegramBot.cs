using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Library
{
    public class TelegramBot : AbstractBot
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
        private ITelegramBotClient Bot;
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
            this.Bot = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }
        public ITelegramBotClient Client
        {
            get
            {
                return this.Bot;
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
        
        
        public override void StartCommunication()
        {
            Bot.OnMessage += OnMessage;
            Bot.StartReceiving();
        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            string chatId = message.Chat.Id.ToString();

            UserMessage msg = new UserMessage(chatId, message.Text);
            SetComunicationChannel(chatId, this);
            TelegramBot.Instance.ManageMessage(msg);

        }

        public override void SendMessage(string id, string message)
        {
            var chatId = long.Parse(id);
            Bot.SendTextMessageAsync(chatId, message);
        }

        public override async void SendFile(string id, string path)
        {
            var chatId = long.Parse(id);
            var fs = System.IO.File.OpenRead(path);
            await Bot.SendDocumentAsync(chatId, new InputOnlineFile(fs, path));
        }
    }
}
