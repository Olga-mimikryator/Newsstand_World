using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.Product Product { get; set; } = new();

        public SelectList TypeList { get; set; } = default!;
        public SelectList PublisherList { get; set; } = default!;

        public void OnGet()
        {
            LoadSelectLists();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private void LoadSelectLists()
        {
            TypeList = new SelectList(_context.ProductTypes, "Id", "Name");
            PublisherList = new SelectList(_context.Publishers, "Id", "Name");
        }
    }
}