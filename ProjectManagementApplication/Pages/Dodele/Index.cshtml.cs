using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Dodele
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<DodelaZadatka> Dodele { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.DodeleZadataka
                .Include(d => d.Aktivnost)
                .Include(d => d.Zaposleni)
                .AsQueryable();

            // Pretraga
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var filter = $"%{CurrentFilter}%";
                query = query.Where(d =>
                    EF.Functions.Like(d.Aktivnost.Naziv, filter) ||
                    EF.Functions.Like(d.Zaposleni.Ime, filter) ||
                    EF.Functions.Like(d.Zaposleni.Prezime, filter));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "aktivnost_asc" => query.OrderBy(d => d.Aktivnost.Naziv),
                "aktivnost_desc" => query.OrderByDescending(d => d.Aktivnost.Naziv),
                "ime_asc" => query.OrderBy(d => d.Zaposleni.Ime),
                "ime_desc" => query.OrderByDescending(d => d.Zaposleni.Ime),
                "prezime_asc" => query.OrderBy(d => d.Zaposleni.Prezime),
                "prezime_desc" => query.OrderByDescending(d => d.Zaposleni.Prezime),
                _ => query.OrderBy(d => d.Aktivnost.Naziv) // podrazumevano po nazivu aktivnosti
            };

            Dodele = await query.ToListAsync();
        }
    }
}
