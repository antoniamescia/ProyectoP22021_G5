using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace BankerBot
{
    //SINGLETON
    public class TelegramBot : AbstractBot
    {
        private ITelegramBotClient Bot;
        private const string Token = "1871185609:AAGlnk0lPpi-ijJZFgsS_jyUIdVDlSHggzw";
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

        private TelegramBot() : base()
        {
            this.Bot = new TelegramBotClient(Token);
        }

        public ITelegramBotClient Client
        {
            get
            {
                return this.Bot;
            }
        }
        public override void StartCommunication()
        {
            Bot.OnMessage += OnMessage;
            Bot.StartReceiving();
        }

        private void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            string chatId = message.Chat.Id.ToString();

            IMessage msg = new BotMessage(chatId, message.Text);
            SetChannel(chatId, this);
            TelegramBot.Instance.HandleMessage(msg);
        }

        public override void SendMessage(string id, string message)
        {

            var chatId = long.Parse(id);
            Bot.SendTextMessageAsync(chatId, message);
        }

        public override async void SendPrint(string id, string path)
        {
            long chatId = long.Parse(id);
            System.IO.FileStream fileStream = System.IO.File.OpenRead(path);
            await Bot.SendDocumentAsync(chatId, new InputOnlineFile(fileStream, path));
        }

    }
}

