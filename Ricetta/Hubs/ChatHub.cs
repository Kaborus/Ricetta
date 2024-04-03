using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;


namespace Ricetta.Hubs
{
    public class ChatHub : Hub
    {

        //private readonly UserManager _userManager;
        public async Task SendMessage(int LobbyId, string user, string message)
        {
            await Clients.Group($"lobby-{LobbyId}").SendAsync("ReceiveMessage", user, message);
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
    }
}