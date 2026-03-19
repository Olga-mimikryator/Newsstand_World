using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;


namespace Newsstand_World.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Newsstand_World.Model.Product> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products
                .Include(p => p.Type)
                .Include(p => p.Publisher)
                .ToList();
        }
    }
}
