using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Publisher
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.Publisher Publisher { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Publishers.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
