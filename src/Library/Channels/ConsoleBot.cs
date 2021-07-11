namespace BankerBot
{
    public class ConsoleBot : AbstractBot
    {
        //SINGLETON
        private static ConsoleBot instance;
        public static ConsoleBot Instance
        {
            get
            {
                if (instance == null) instance = new ConsoleBot();

                return instance;
            }
        }
        private ConsoleBot() : base()
        { }
        public override void StartCommunication()
        {
            System.Console.WriteLine("Recuerda que puedes escribir \"Salir\" en cualquier momento para finalizar la conversación. 👋🏼");
            while (true)
            {
                string text = System.Console.ReadLine().ToString();
                if (text == "Salir") return;
                SetChannel("Console", this);
                BotMessage message = new BotMessage("Console", text);
                HandleMessage(message);
            }
        }

        public override void SendMessage(string id, string message)
        {
            System.Console.WriteLine(message);
        }

        public override void SendPrint(string id, string path)
        {
            System.Console.WriteLine("Para ver su historial de transacciones ingrese en el archivo que se encuentra en: " + path);
        }
    }
}

