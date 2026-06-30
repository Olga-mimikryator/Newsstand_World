using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.Product Product { get; set; } = default!;

        public SelectList TypeList { get; set; } = default!;
        public SelectList PublisherList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products
                .Include(p => p.Type)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            LoadSelectLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(Product.Id);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = Product.Name;
            productToUpdate.TypeID = Product.TypeID;
            productToUpdate.PublisherID = Product.PublisherID;
            productToUpdate.Price = Product.Price;
            productToUpdate.Quantity = Product.Quantity;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void LoadSelectLists()
        {
            TypeList = new SelectList(_context.ProductTypes, "Id", "Name");
            PublisherList = new SelectList(_context.Publishers, "Id", "Name");
        }
    }
}