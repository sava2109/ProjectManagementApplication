using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages
{
    public class AktivnostiViseZaposlenihModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public AktivnostiViseZaposlenihModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public class AktivnostSaZaposlenima
        {
            public Aktivnost Aktivnost { get; set; } = null!;
            public List<Zaposleni> Zaposleni { get; set; } = new List<Zaposleni>();
        }

        public List<AktivnostSaZaposlenima> AktivnostiLista { get; set; } = new List<AktivnostSaZaposlenima>();

        public async Task OnGetAsync()
        {
            // Dohvat aktivnosti koje imaju više od jednog zaposlenog
            var aktivnosti = await _context.Aktivnosti
                .Include(a => a.Dodele)
                .ThenInclude(d => d.Zaposleni)
                .Where(a => a.Dodele.Count > 1)
                .ToListAsync();

            AktivnostiLista = aktivnosti.Select(a => new AktivnostSaZaposlenima
            {
                Aktivnost = a,
                Zaposleni = a.Dodele.Select(d => d.Zaposleni).ToList()
            }).ToList();
        }
    }
}
