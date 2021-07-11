namespace BankerBot
{
    public class ConsoleBot : AbstractBot
    {
        /*
        Patrones y principios:
        Cumple con SRP porque solo se identifica una razón de cambio.
        Cumple con Expert porque tiene toda la información necesaria para poder cumplir con la responsabilidad otorgada.
        Cumple con Polymorphism porque usa los métodos polimórficos StartCommunication y SendMessage.
        Cumple con el patrón Creator al crear objetos del tipo IMessage pues usa de forma directa dichas instancias (es el encargado de enviarle mensajes al usuario).
        Cumple con el patrón Singleton: garantiza que haya una única instancia de la clase y proporciona un punto de acceso global a esta instancia.
        */
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
                IMessage message = new BotMessage("Console", text);
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

