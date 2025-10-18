using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.ClanoviTima
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<ClanTima> Clanovi { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.ClanoviTimova
                .Include(c => c.Tim)
                .Include(c => c.Zaposleni)
                .AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var filter = $"%{CurrentFilter}%";
                query = query.Where(c =>
                    EF.Functions.Like(c.Tim.Naziv, filter) ||
                    EF.Functions.Like(c.Zaposleni.Ime, filter) ||
                    EF.Functions.Like(c.Zaposleni.Prezime, filter));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "tim_asc" => query.OrderBy(c => c.Tim.Naziv),
                "tim_desc" => query.OrderByDescending(c => c.Tim.Naziv),
                "ime_asc" => query.OrderBy(c => c.Zaposleni.Ime),
                "ime_desc" => query.OrderByDescending(c => c.Zaposleni.Ime),
                "prezime_asc" => query.OrderBy(c => c.Zaposleni.Prezime),
                "prezime_desc" => query.OrderByDescending(c => c.Zaposleni.Prezime),
                _ => query.OrderBy(c => c.Tim.Naziv) // podrazumevano rastuce po timu
            };

            Clanovi = await query.ToListAsync();
        }
    }
}
