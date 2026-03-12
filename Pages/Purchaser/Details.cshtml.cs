using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Purchaser
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Newsstand_World.Model.Purchaser Purchaser { get; set; }

        public IActionResult OnGet(int id)
        {
            Purchaser = _context.Purchasers.FirstOrDefault(p => p.Id == id);

            if (Purchaser == null)
                return NotFound();

            return Page();
        }
    }
}
