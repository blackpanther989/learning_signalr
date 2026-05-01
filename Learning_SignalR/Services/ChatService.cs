namespace Learning_SignalR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

public interface IChatService
{
    Task InitializeAsync();
    Task SendMessage(string message);
    Task GetAllMessages();
    bool IsConnected { get; }
    IReadOnlyList<string> Messages { get; }
    event Action? OnMessageReceived;
}

public class ChatService(NavigationManager navigationManager) : IChatService, IAsyncDisposable
{
    private static readonly List<string> messages = [];
    private HubConnection? hubConnection;

    public bool IsConnected =>
        hubConnection?.State == HubConnectionState.Connected;

    public IReadOnlyList<string> Messages => messages.AsReadOnly();

    public event Action? OnMessageReceived;

    public async Task InitializeAsync()
    {
        if (hubConnection is not null) return;

        hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string>("ReceiveMessage", (msg) =>
        {
            //messages.Add(msg);
            OnMessageReceived?.Invoke();
        });

        hubConnection.On<List<string>>("SendAllMessages", (msgs) =>
        {
         //   messages.AddRange(msgs);
        });
        
        await hubConnection.StartAsync();
        await hubConnection.SendAsync("Connect");
    }

    public async Task GetAllMessages()
    {
        if (hubConnection is not null && IsConnected)
        {
            await hubConnection.SendAsync("SendAllMessages", messages);
        }
    }
    
    public async Task SendMessage(string message)
    {
        if (hubConnection is not null && IsConnected)
        {
            messages.Add(message);
            await hubConnection.SendAsync("SendMessage", message);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }
}