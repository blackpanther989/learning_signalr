namespace Learning_SignalR;

using Microsoft.AspNetCore.SignalR;

public class CountHub : Hub
{
    public async Task SendCount(int Count)
    {
        await Clients.Others.SendAsync("ReceiveCount", Count);
    }
}