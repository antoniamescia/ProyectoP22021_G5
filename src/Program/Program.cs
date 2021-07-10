using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BankerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.StartCommunication();
            // //Configuration.Start();
            // //Obtengo una instancia de TelegramBot
            // TelegramBot telegramBot = TelegramBot.Instance;
            // Console.WriteLine($"Hola soy el Bot de P2, mi nombre es {telegramBot.BotName} y tengo el Identificador {telegramBot.BotId}");

            // //Obtengo el cliente de Telegram
            // ITelegramBotClient bot = telegramBot.Client;

            // //Asigno un gestor de mensajes
            // bot.OnMessage += OnMessage;

            // //Inicio la escucha de mensajes
            // bot.StartReceiving();


            // Console.WriteLine("Presiona una tecla para terminar");
            // Console.ReadKey();

            // //Detengo la escucha de mensajes 
            // bot.StopReceiving();
        }
    }
}