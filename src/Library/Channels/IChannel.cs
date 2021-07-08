namespace Bankbot
{
    /*Cumple con ## OCP ## ya que se pueden agregar los canales que se deseen agregando cosas mínimas sin necesidad de 
    cambiar el código fuente.*/
    /// <summary>
    /// Recibe un canal para interactuar con un usuario.
    /// </summary>
    public interface IChannel
    {
        void Start();
        void HandleMessage(IMessage message);
        void SendMessage(string id, string message);

    }
}