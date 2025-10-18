using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementApplication.Pages.RadniPaketi
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<RadniPaket> RadniPaketi { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.RadniPaketi.Include(rp => rp.Projekat).AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var filter = $"%{CurrentFilter}%";
                query = query.Where(rp => EF.Functions.Like(rp.Naziv, filter) ||
                                          EF.Functions.Like(rp.Opis ?? "", filter));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "naziv_desc" => query.OrderByDescending(rp => rp.Naziv),
                "planirani" => query.OrderBy(rp => rp.PlaniraniSati),
                "planirani_desc" => query.OrderByDescending(rp => rp.PlaniraniSati),
                _ => query.OrderBy(rp => rp.Naziv)
            };

            RadniPaketi = await query.ToListAsync();
        }
    }
}
