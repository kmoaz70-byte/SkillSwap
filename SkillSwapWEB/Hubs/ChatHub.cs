using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;


namespace SkillSwapWEB.Hubs
{
  

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string receiverId, string content)
        {
            string senderId = Context.UserIdentifier!;

            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);
            await Clients.Caller.SendAsync("ReceiveMessage", senderId, content);
        }
    }
}
