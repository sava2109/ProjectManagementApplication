using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.ZaposleniPages
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<Zaposleni> ZaposleniLista { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Zaposleni.AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(z => EF.Functions.Like(z.Ime, $"%{SearchString}%") ||
                                         EF.Functions.Like(z.Prezime, $"%{SearchString}%"));
            }

            // Sortiranje
            ZaposleniLista = SortOrder switch
            {
                "ime_desc" => await query.OrderByDescending(z => z.Ime).ToListAsync(),
                "ime" => await query.OrderBy(z => z.Ime).ToListAsync(),
                "prezime" => await query.OrderBy(z => z.Prezime).ToListAsync(),
                "prezime_desc" => await query.OrderByDescending(z => z.Prezime).ToListAsync(),
                _ => await query.OrderBy(z => z.Ime).ToListAsync(),
            };
        }
    }
}
