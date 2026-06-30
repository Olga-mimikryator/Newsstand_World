using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Account.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuthUser User { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // ѕровер€ем, есть ли уже такой пользователь
            if (_context.AuthUsers.Any(u => u.Email == User.Email))
            {
                ModelState.AddModelError("User.Email", "ѕользователь с таким Email уже существует");
                return Page();
            }

            _context.AuthUsers.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}