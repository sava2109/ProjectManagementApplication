using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementApplication.Pages.Aktivnosti
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<Aktivnost> Aktivnosti { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Aktivnosti.Include(a => a.Zadatak).AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var filter = $"%{CurrentFilter}%";
                query = query.Where(a => EF.Functions.Like(a.Naziv, filter) ||
                                         EF.Functions.Like(a.Opis ?? "", filter));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "naziv_desc" => query.OrderByDescending(a => a.Naziv),
                "sati" => query.OrderBy(a => a.PlaniraniSati),
                "sati_desc" => query.OrderByDescending(a => a.PlaniraniSati),
                _ => query.OrderBy(a => a.Naziv)
            };

            Aktivnosti = await query.ToListAsync();
        }
    }
}
