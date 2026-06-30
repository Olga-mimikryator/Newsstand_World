using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Account.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
            var user = await _context.AuthUsers.FindAsync(User.Id);

            if (user != null)
            {
                // Защита от удаления админа по умолчанию
                if (user.Email == "admin@newsstand.ru")
                    return RedirectToPage("Index");

                _context.AuthUsers.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}