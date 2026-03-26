using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.ProductType
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.ProductType ProductType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProductType = await _context.ProductTypes.FindAsync(id);

            if (ProductType == null)
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

            var typeToUpdate = await _context.ProductTypes.FindAsync(ProductType.Id);
            if (typeToUpdate == null)
            {
                return NotFound();
            }

            typeToUpdate.Name = ProductType.Name;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
