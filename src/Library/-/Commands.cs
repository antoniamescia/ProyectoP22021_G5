using System;
using System.Collections.Generic;
using System.Text;

namespace Bankbot
{
    public class Commands
    {
        public List<string> CommandsList { get; set; }
        private static Commands instance;

        public static Commands Instance
        {
            get
            {
                if (instance == null) instance = new Commands();
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
                "/crearusuario",
                "/crearcuenta",
                "/transaccion",
                "/convertir",
                "/agregarmoneda",
                "/mostrarbalance",
                "/agregarcategoriadegasto",
                "/cambiarobjetivodeahorro",
                "/salir"
            };
        }
        public string CommandList(string id)
        {
            var data = Session.Instance.GetChat(id);

            string commandList = string.Empty;

            if (data.User == null)
            {
                foreach (string command in UnlogedCommandsList())
                {
                    commandList += command + "\n";
                }
                return commandList;
            }
            else if (data.User.Accounts.Count == 0)
            {
                foreach (string command in HasNoAccountsCommandsList())
                {
                    commandList += command + "\n";
                }
                return commandList;
            }

            foreach (string command in HasAccountCommandsList())
            {
                commandList += command + "\n";
            }

            return commandList;
        }
        private static List<string> UnlogedCommandsList()
        {
            List<string> unlogedList = new List<string>();
            unlogedList.Add("/IniciarSesion");
            unlogedList.Add("/CrearUsuario");
            return unlogedList;
        }
        private static List<string> HasNoAccountsCommandsList()
        {
            List<string> hasNoAccountsList = new List<string>();
            hasNoAccountsList.Add("/CerrarSesion");
            hasNoAccountsList.Add("/CrearCuenta");
            hasNoAccountsList.Add("/Convertir");
            hasNoAccountsList.Add("/CrearUsuario");
            return hasNoAccountsList;
        }
        private static List<string> HasAccountCommandsList()
        {
            List<string> hasAccountsList = new List<string>();
            hasAccountsList.Add("/CerrasSesion");
            hasAccountsList.Add("/CrearCuenta");
            hasAccountsList.Add("/Convertir");
            hasAccountsList.Add("/CrearUsuario");
            hasAccountsList.Add("/MostrarBalance");
            hasAccountsList.Add("/Transaccion");
            hasAccountsList.Add("/AgregarCategoriaDeGasto");
            hasAccountsList.Add("/CambiarObjetivoDeAhorro");
            return hasAccountsList;
        }

        public bool CommandExist(string command)
        {
            return instance.CommandsList.Contains(command.ToLower());
        }
    }
}
