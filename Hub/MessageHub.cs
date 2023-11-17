using Microsoft.AspNetCore.SignalR;
using WebApplication1.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Dto;

namespace WebApplication1.Hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        private static readonly List<string> ConnectedUsers = new List<string>();



        public async Task SendOffersToUser(string data)
        {
            await Clients.All.SendOffersToUser(data);
        }

        public async Task SendListUsers(List<User> data)
        {
            await Clients.All.SendListUsers(data);
        }









        

        public override async Task OnConnectedAsync()
        {
            // L'utilisateur est connecté
            var connectionId = Context.ConnectionId;
            ConnectedUsers.Add(connectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // L'utilisateur est déconnecté
            var connectionId = Context.ConnectionId;
            ConnectedUsers.Remove(connectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public List<string> GetConnectedUsers()
        {
            return ConnectedUsers;
        }









    }
}
