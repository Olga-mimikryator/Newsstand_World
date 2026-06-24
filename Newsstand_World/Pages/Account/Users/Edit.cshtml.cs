using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Account.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuthUser User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.AuthUsers.FindAsync(id);

            if (User == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Загружаем существующего пользователя
            var existingUser = await _context.AuthUsers.FindAsync(User.Id);
            if (existingUser == null)
                return NotFound();

            // Обновляем поля
            existingUser.Email = User.Email;
            existingUser.Role = User.Role;

            // Если пароль указан, обновляем его
            if (!string.IsNullOrEmpty(User.Password))
            {
                existingUser.Password = User.Password;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}