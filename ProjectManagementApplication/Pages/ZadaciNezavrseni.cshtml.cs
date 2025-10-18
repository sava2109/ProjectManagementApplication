using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages
{
    public class ZadaciNezavrseniModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public ZadaciNezavrseniModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public class ZadatakSaStatusom
        {
            public Zadatak Zadatak { get; set; } = null!;
            public int UkupnoAktivnosti { get; set; }
            public int NezavrseneAktivnosti { get; set; }
            public double ProcenatNezavrsenih { get; set; }
        }

        public List<ZadatakSaStatusom> ZadaciLista { get; set; } = new List<ZadatakSaStatusom>();

        public async Task OnGetAsync()
        {
            var zadaci = await _context.Zadaci
                .Include(z => z.Aktivnosti)
                .ToListAsync();

            ZadaciLista = zadaci
                .Select(z =>
                {
                    var ukupno = z.Aktivnosti.Count;
                    var nezavrsene = z.Aktivnosti.Count(a => a.Status.ToLower() != "završeno");
                    var procenatNezavrsenih = ukupno == 0 ? 0 : (double)nezavrsene / ukupno * 100;

                    return new ZadatakSaStatusom
                    {
                        Zadatak = z,
                        UkupnoAktivnosti = ukupno,
                        NezavrseneAktivnosti = nezavrsene,
                        ProcenatNezavrsenih = procenatNezavrsenih
                    };
                })
                .Where(zs => zs.ProcenatNezavrsenih > 50) // više od 50% aktivnosti nije završeno
                .ToList();
        }
    }
}
