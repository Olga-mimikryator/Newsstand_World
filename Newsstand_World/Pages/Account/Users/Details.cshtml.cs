using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Account.Users
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthUser User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.AuthUsers.FindAsync(id);

            if (User == null)
                return NotFound();

            return Page();
        }
    }
}