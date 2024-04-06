using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ricetta.Data;
using Ricetta.Data.Entities;

namespace Ricetta.Hubs
{
    public class ChatHub : Hub
    {
        //private readonly UserManager _userManager;
        public async Task SendMessage(int LobbyId, string user, string message)
        {
            await Clients.Group($"lobby-{LobbyId}").SendAsync("ReceiveMessage", user, message);
            //Message chat = new Message() { InboxId = LobbyId, SenderId = user, Content = message };
        }

        public async Task JoinGroup(int LobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"lobby-{LobbyId}");
        }

        public async Task LeaveGroup(int LobbyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"lobby-{LobbyId}");
        }

        public async Task ShowName(string user, int LobbyId)
        {
            await Clients.Group($"lobby-{LobbyId}").SendAsync("ReceivePlayer", user);
        }

        public async Task SendNotification(string recipeName, string categoryName)
        {
            await Clients.All.SendAsync("RecipeCreated", recipeName, categoryName);
        }
    }
}