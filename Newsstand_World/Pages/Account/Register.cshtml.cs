using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;
using System.Security.Claims;

namespace Newsstand_World.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Register Input { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Проверяем, есть ли уже такой пользователь
            var existingUser = _context.AuthUsers.FirstOrDefault(u => u.Email == Input.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким Email уже существует");
                return Page();
            }

            // Если это первый пользователь в системе, делаем его админом
            bool isFirstUser = !_context.AuthUsers.Any();

            var user = new AuthUser
            {
                Email = Input.Email,
                Password = Input.Password,
                Role = isFirstUser ? "Admin" : "User"
            };

            _context.AuthUsers.Add(user);
            await _context.SaveChangesAsync();

            // Автоматически входим после регистрации
            await Authenticate(user.Email, user.Role);
            return RedirectToPage("/Index");
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
