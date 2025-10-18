using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementApplication.Pages.Zadaci
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<Zadatak> Zadaci { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Zadaci.Include(z => z.RadniPaket).AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var filter = $"%{CurrentFilter}%";
                query = query.Where(z => EF.Functions.Like(z.Naziv, filter) ||
                                         EF.Functions.Like(z.Opis ?? "", filter));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "naziv_desc" => query.OrderByDescending(z => z.Naziv),
                "planirani" => query.OrderBy(z => z.PlaniraniSati),
                "planirani_desc" => query.OrderByDescending(z => z.PlaniraniSati),
                _ => query.OrderBy(z => z.Naziv)
            };

            Zadaci = await query.ToListAsync();
        }
    }
}
