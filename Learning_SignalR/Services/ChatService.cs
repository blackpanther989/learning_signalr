using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace Learning_SignalR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

public interface IChatService
{
    Task SendMessage(string message);
    Task<List<string>>  GetAllMessages();
    IReadOnlyList<string> Messages { get; }
}

public class ChatService(IHubContext<ChatHub> _hubContext) : IChatService
{
    private static readonly ConcurrentQueue<string> messages = [];
  
    public IReadOnlyList<string> Messages => messages.ToList().AsReadOnly();

    public Task<List<string>> GetAllMessages() => Task.FromResult(messages.ToList());
    public event Action? OnMessageReceived;

    
    public async Task SendMessage(string message)
    {
        messages.Enqueue(message);
        OnMessageReceived?.Invoke();
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

    }
    
}