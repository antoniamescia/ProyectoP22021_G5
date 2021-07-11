using System;
using System.Collections.Generic;
using System.Text;

namespace BankerBot
{
    public class Commands
    {
        /*
        Patrones y principios:
        Cumple con SRP pues no se identifica más de una razón de cambio. 
        Cumple con Expert pues el experto en la informació necesaria para llevar a cabo las responsabilidades asignadas. 
        Cumple con el patrón Singleton: garantiza que haya una única instancia de la clase y proporciona un punto de acceso global a esta instancia.
        */
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
        /// <summary>
        /// Crea lista de comandos
        /// </summary>
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
                "/salir",
                "/verhistorialdetransacciones"
            };
        }

        public string CommandList(string id)
        {
            UserInfo data = Session.Instance.GetChatInfo(id);

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

        public bool CommandExists(string command)
        {
            return instance.CommandsList.Contains(command.ToLower());
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
            hasAccountsList.Add("/CerrarSesion");
            hasAccountsList.Add("/CrearCuenta");
            hasAccountsList.Add("/Convertir");
            hasAccountsList.Add("/CrearUsuario");
            hasAccountsList.Add("/MostrarBalance");
            hasAccountsList.Add("/Transaccion");
            hasAccountsList.Add("/AgregarCategoriaDeGasto");
            hasAccountsList.Add("/CambiarObjetivoDeAhorro");
            hasAccountsList.Add("/VerHistorialDeTransacciones");
            return hasAccountsList;
        }

        private static List<string> UnlogedCommandsList()
        {
            List<string> unlogedList = new List<string>();
            unlogedList.Add("/IniciarSesion");
            unlogedList.Add("/CrearUsuario");
            return unlogedList;
        }
    }
}
