using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Purchaser
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.Purchaser Purchaser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Purchaser = await _context.Purchasers.FindAsync(id);

            if (Purchaser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var purchaser = await _context.Purchasers.FindAsync(id);
            if (purchaser != null)
            {
                _context.Purchasers.Remove(purchaser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
