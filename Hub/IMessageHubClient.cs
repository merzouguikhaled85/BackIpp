using System;
using WebApplication1.Data;
using WebApplication1.Dto;

namespace WebApplication1.Hub
{
    public interface IMessageHubClient
    {
       
        Task SendOffersToUser(string message);

        Task SendListUsers(List<User> data);


        // Ajoutez cette méthode
        Task SendAsync(string methodName, params object[] args);








    }
}
