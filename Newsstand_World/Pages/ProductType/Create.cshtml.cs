using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newsstand_World.Data;
using Newsstand_World.Hubs;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.ProductType
{
    [Authorize(Roles = "Admin,Manager")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Newsstand_World.Model.ProductType ProductType { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductTypes.Add(ProductType);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}