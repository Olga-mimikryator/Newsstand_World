using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Newsstand_World.Pages
{
    public class ChatModel : PageModel
    {
        public string UserName { get; set; } = "Аноним";
        public string UserEmail { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        public void OnGet()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                UserName = User.Identity.Name!;
                UserEmail = User.Identity.Name!;
                Role = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
            }
        }
    }
}