using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Publisher
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Publisher = await _context.Publishers.FindAsync(id);

            if (Publisher == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var publisherToUpdate = await _context.Publishers.FindAsync(Publisher.Id);
            if (publisherToUpdate == null)
            {
                return NotFound();
            }

            publisherToUpdate.Name = Publisher.Name;
            publisherToUpdate.Phone = Publisher.Phone;
            publisherToUpdate.Email = Publisher.Email;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
