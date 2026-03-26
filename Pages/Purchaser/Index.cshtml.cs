using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newsstand_World.Data;

namespace Newsstand_World.Pages.Purchaser
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Newsstand_World.Model.Purchaser> Purchasers { get; set; }

        public void OnGet()
        {
            Purchasers = _context.Purchasers.ToList();
        }
    }
}
