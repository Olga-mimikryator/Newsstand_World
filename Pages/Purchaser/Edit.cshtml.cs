using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Purchaser
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var purchaserToUpdate = await _context.Purchasers.FindAsync(Purchaser.Id);
            if (purchaserToUpdate == null)
            {
                return NotFound();
            }

            purchaserToUpdate.Name = Purchaser.Name;
            purchaserToUpdate.LastName = Purchaser.LastName;
            purchaserToUpdate.Phone = Purchaser.Phone;
            purchaserToUpdate.BirthDate = Purchaser.BirthDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
