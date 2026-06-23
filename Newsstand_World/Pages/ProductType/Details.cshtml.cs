using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.ProductType
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Newsstand_World.Model.ProductType ProductType { get; set; } = default!;
        public List<Newsstand_World.Model.Product> Products { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ProductType = await _context.ProductTypes.FindAsync(id);

            if (ProductType == null)
            {
                return NotFound();
            }

            Products = await _context.Products
                .Where(p => p.TypeID == id)
                .ToListAsync();

            return Page();
        }
    }
}