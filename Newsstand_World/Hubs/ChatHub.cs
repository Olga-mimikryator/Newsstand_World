using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Newsstand_World.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> _userConnections = new();
        private static readonly List<ChatMessage> _messages = new();

        public override async Task OnConnectedAsync()
        {
            var userEmail = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userEmail))
            {
                _userConnections[userEmail] = Context.ConnectionId;

                // Отправляем историю сообщений
                await Clients.Caller.SendAsync("LoadHistory", _messages.TakeLast(50));

                // Обновляем список онлайн для всех
                await Clients.All.SendAsync("UpdateOnlineUsers", _userConnections.Keys.ToList());
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userEmail = Context.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userEmail) && _userConnections.ContainsKey(userEmail))
            {
                _userConnections.Remove(userEmail);
                await Clients.All.SendAsync("UpdateOnlineUsers", _userConnections.Keys.ToList());
            }
            await base.OnDisconnectedAsync(exception);
        }

        // Метод для получения списка онлайн пользователей
        public Task<List<string>> GetOnlineUsers()
        {
            return Task.FromResult(_userConnections.Keys.ToList());
        }

        // Только отправка сообщений пользователями
        public async Task SendMessage(string message)
        {
            var userEmail = Context.User?.Identity?.Name ?? "Аноним";
            var userName = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? userEmail;
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value ?? "User";

            if (string.IsNullOrEmpty(message)) return;

            var chatMessage = new ChatMessage
            {
                Id = _messages.Count + 1,
                UserEmail = userEmail,
                UserName = userName,
                Role = role,
                Message = message,
                Timestamp = DateTime.Now
            };

            _messages.Add(chatMessage);
            if (_messages.Count > 100) _messages.RemoveAt(0);

            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }
    }

    public class ChatMessage
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}