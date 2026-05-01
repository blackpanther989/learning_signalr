namespace Learning_SignalR;

using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendMessage(string msg)
    {
        await Clients.Others.SendAsync("ReceiveMessage", msg);
    }
    
    public async Task SendAllMessages(List<String> messages)
    {
        await Clients.Caller.SendAsync("SendAllMessages", messages);
    }
}