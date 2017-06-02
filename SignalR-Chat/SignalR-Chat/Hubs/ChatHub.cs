using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Chat.Hubs
{

    public class ChatHub : Hub
    {
        public static List<Client> ConnectedUsers { get; set; } = new List<Client>();
        public static List<string> Grupos { get; set; } = new List<string>();

        public static List<string> Mensajes { get; set; } = new List<string>();

        public void Connect(string username)
        {
            string[] param = username.Split('_');
            string grupo = param[1];
            string user = param[0];

 
            Groups.Add(Context.ConnectionId, grupo);
            Grupos.Add(grupo);


            Client c = new Client()
            {
                Username = user,
                ID = Context.ConnectionId,
                GrupoId = grupo
            };
            if (!ConnectedUsers.Exists(p=>p.ID == c.ID))
            {
                ConnectedUsers.Add(c);
                //grupos


                Clients.All.updateGrupos(ConnectedUsers.Count(), Grupos.Distinct().ToList().Count(), Grupos.Distinct().ToList(), Mensajes.Count(), Mensajes.ToArray());
                Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.Username));
                //Clients.Group(grupo).up

            }
            
        }


        public void Send(string message)
        {
            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            Clients.Group(sender.GrupoId).broadcastMessage(sender.Username, message);
            Mensajes.Add(message);
            //Clients.All.broadcastMessage(sender.Username, message);
        }
        public void SendUrl(string message, string url, string rol)
        {
            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            if (rol != "9")
                Clients.Group(sender.GrupoId).broadcastMessageUrl(sender.Username, message, url);
            else
                Clients.Group(sender.GrupoId).broadcastMessage(sender.Username, message);
            //Clients.All.broadcastMessage(sender.Username, message);
            Mensajes.Add(message);
        }
        public void SendMensaje(string message, string url, string tipo, string rol, string result)
        {
            //debemos evaluar el tipo de mensaje para saber si enviarlo o no
            //por el momento esta vamos a considerar solo el rol
            //si el rol es apoderado no se mostrará la url
            //tipo 1 crear modificar Institucion
            //tipo 2 eliminar institucion
            //tipo 3 crear modificar rendicion
            //tipo  eliminar rendicion

            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            if (rol != "9")
                Clients.Group(sender.GrupoId).broadcastMessageUrl(sender.Username, message, url);
            else
                Clients.Group(sender.GrupoId).broadcastMessage(sender.Username, message);
            Mensajes.Add(message);
            //Clients.All.broadcastMessage(sender.Username, message);
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var disconnectedUser = ConnectedUsers.FirstOrDefault(c => c.ID.Equals(Context.ConnectionId));
            ConnectedUsers.Remove(disconnectedUser);
            Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.Username));
            return base.OnDisconnected(stopCalled);
        }
    }


    public class Client
    {
        public string Username { get; set; }

        public string ID { get; set; }

        public string GrupoId { get; set; }
    }

}
