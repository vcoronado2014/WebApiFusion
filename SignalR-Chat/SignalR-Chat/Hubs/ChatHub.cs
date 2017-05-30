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

        public void Connect(string username)
        {
            string[] param = username.Split('_');
            string grupo = param[1];
            string user = param[0];

 
            Groups.Add(Context.ConnectionId, grupo);


            Client c = new Client()
            {
                Username = user,
                ID = Context.ConnectionId,
                GrupoId = grupo
            };
            if (!ConnectedUsers.Exists(p=>p.ID == c.ID))
            {
                ConnectedUsers.Add(c);
                
                Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.Username));
                //Clients.Group(grupo).up
            }
            
        }


        public void Send(string message)
        {
            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            Clients.Group(sender.GrupoId).broadcastMessage(sender.Username, message);
            //Clients.All.broadcastMessage(sender.Username, message);
        }
        public void SendUrl(string message, string url)
        {
            var sender = ConnectedUsers.First(u => u.ID.Equals(Context.ConnectionId));
            Clients.Group(sender.GrupoId).broadcastMessageUrl(sender.Username, message, url);
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
