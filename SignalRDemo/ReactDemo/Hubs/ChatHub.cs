using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactDemo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public struct WebSocketActions
    {
        public static readonly string MESSAGE_RECEIVED = "messageReceived";
        public static readonly string USER_LEFT = "userLeft";
        public static readonly string USER_JOINED = "userJoined";
    }
}
