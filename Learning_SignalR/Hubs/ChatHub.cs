namespace Learning_SignalR;

using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendCount(int Count)
    {
        await Clients.Others.SendAsync("ReceiveCount", Count);
    }
}