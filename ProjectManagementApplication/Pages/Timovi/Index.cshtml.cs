using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Timovi
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<Tim> TimoviLista { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Timovi.Include(t => t.Projekat).AsQueryable();

            // Pretraga po nazivu tima
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(t => EF.Functions.Like(t.Naziv, $"%{SearchString}%"));
            }

            // Sortiranje
            TimoviLista = SortOrder switch
            {
                "naziv_desc" => await query.OrderByDescending(t => t.Naziv).ToListAsync(),
                "naziv" => await query.OrderBy(t => t.Naziv).ToListAsync(),
                _ => await query.OrderBy(t => t.Naziv).ToListAsync(),
            };
        }
    }
}
