using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.ProductType
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Newsstand_World.Model.ProductType> ProductTypes { get; set; } = new();

        public void OnGet()
        {
            ProductTypes = _context.ProductTypes.ToList();
        }
    }
}
