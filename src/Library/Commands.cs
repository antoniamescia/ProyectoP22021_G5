using System;
using System.Collections.Generic;

namespace BankerBot
{
    public class Commands
    {
        public List<string> CommandsList { get; set; }
        private static Commands instance;
        public static Commands Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Commands();

                }
                return instance;
            }
        }
        private Commands()
        {
            this.CommandsList = new List<string>() 
            {
                "/comandos",
                "/iniciarsesion",
                "/cerrarsesion",
                "/borrarusuario",
                "/crearusuario",
                "/crearcuenta",
                "/borrarcuenta",
                "/transaccion",
                "/convertir",
                "/agregarmoneda",
                "/balance",
                "/agregarcategoriadegasto",
                "/cambiarobjetivodeahorro",
                "/salir"
            };
            
        }


        public string CommandList(string id)
        {
            var data = Session.Instance.GetChatInfo(id);

            string commandList = string.Empty;

            if (data.User == null)
            {
                foreach (string command in UnloggedCommands())
                {
                    commandList += command + "\n";
                }
                return commandList;
            }
            else if (data.User.Accounts.Count == 0)
            {
                foreach (string command in NoAccountsCommands())
                {
                    commandList += command + "\n";
                }
                return commandList;
            }

            foreach (string command in HasAccountCommands())
            {
                commandList += command + "\n";
            }

            return commandList;
        }
        private static List<string> UnloggedCommands()
        {
            List<string> unlogged = new List<string>();
            unlogged.Add("/IniciarSesion");
            unlogged.Add("/CrearUsuario");
            return unlogged;
        }
        private static List<string> NoAccountsCommands()
        {
            List<string> noAccounts = new List<string>();
            noAccounts.Add("/CerrarSesion");
            // noAccounts.Add("/DeleteUser");
            noAccounts.Add("/CrearCuenta");
            noAccounts.Add("/Convertir");
            noAccounts.Add("/CrearUsuario");
            return noAccounts;
        }
        private static List<string> HasAccountCommands()
        {
            List<string> hasAccountsList = new List<string>();
            hasAccountsList.Add("/CerrarSesion");
            // hasAccountsList.Add("/DeleteUser");
            hasAccountsList.Add("/CrearCuenta");
            hasAccountsList.Add("/Convertir");
            hasAccountsList.Add("/CrearUsuario");
            hasAccountsList.Add("/BorrarCuenta");
            hasAccountsList.Add("/MostrarBalance");
            hasAccountsList.Add("/Transaccion");
            hasAccountsList.Add("/AgregarCategoriaDeGasto");
            hasAccountsList.Add("/CambiarObjetivoDeAhorro");
            hasAccountsList.Add("/AgregarMoneda");
            return hasAccountsList;
        }

        public bool CommandExists(string command)
        {
            return instance.CommandsList.Contains(command.ToLower());
        }
    }
}