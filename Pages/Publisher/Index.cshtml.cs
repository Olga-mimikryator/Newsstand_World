using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newsstand_World.Data;
using Newsstand_World.Model;

namespace Newsstand_World.Pages.Publisher
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Newsstand_World.Model.Publisher> Publishers { get; set; } = new();

        public void OnGet()
        {
            Publishers = _context.Publishers.ToList();
        }
    }
}
