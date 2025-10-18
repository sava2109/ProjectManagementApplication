using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementApplication.Pages.Projekti
{
    public class IndexModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public IndexModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public IList<Projekat> Projekti { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Projekti.AsQueryable();

            // Pretraga (EF.Functions.Like radi direktno u SQL)
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                var search = $"%{CurrentFilter}%";
                query = query.Where(p => EF.Functions.Like(p.Naziv, search) ||
                                         EF.Functions.Like(p.Opis ?? "", search));
            }

            // Sortiranje
            query = SortOrder switch
            {
                "naziv_desc" => query.OrderByDescending(p => p.Naziv),
                "planirani" => query.OrderBy(p => p.PlaniraniSati),
                "planirani_desc" => query.OrderByDescending(p => p.PlaniraniSati),
                _ => query.OrderBy(p => p.Naziv)
            };

            Projekti = await query.ToListAsync();
        }
    }
}
