using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Publisher
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Newsstand_World.Model.Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Publisher = await _context.Publishers
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Publisher == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
