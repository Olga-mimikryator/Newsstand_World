using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.ProductType
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.ProductType ProductType { get; set; } = default!;

        public bool HasRelatedProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProductType = await _context.ProductTypes.FindAsync(id);

            if (ProductType == null)
            {
                return NotFound();
            }

            HasRelatedProducts = await _context.Products.AnyAsync(p => p.TypeID == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var type = await _context.ProductTypes.FindAsync(id);
            if (type != null)
            {
                _context.ProductTypes.Remove(type);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
