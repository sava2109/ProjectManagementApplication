using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Izvestaji
{
    public class ProjekatModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public ProjekatModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? ProjekatId { get; set; }

        public List<Projekat> Projekti { get; set; } = new();
        public Projekat? IzabraniProjekat { get; set; }

        public int UkupnoRadnihPaketa { get; set; }
        public int UkupnoZadataka { get; set; }
        public int UkupnoAktivnosti { get; set; }

        public int? IzabraniProjekatId => ProjekatId;

        public async Task OnGetAsync()
        {
            // Učitavanje projekata za dropdown
            Projekti = await _context.Projekti.ToListAsync();

            if (ProjekatId.HasValue)
            {
                IzabraniProjekat = await _context.Projekti
                    .Include(p => p.RadniPaketi)
                        .ThenInclude(rp => rp.Zadatci)
                            .ThenInclude(z => z.Aktivnosti)
                                .ThenInclude(a => a.Dodele)
                                    .ThenInclude(d => d.Zaposleni)
                    .FirstOrDefaultAsync(p => p.Id == ProjekatId.Value);

                if (IzabraniProjekat != null)
                {
                    UkupnoRadnihPaketa = IzabraniProjekat.RadniPaketi.Count;
                    UkupnoZadataka = IzabraniProjekat.RadniPaketi.SelectMany(rp => rp.Zadatci).Count();
                    UkupnoAktivnosti = IzabraniProjekat.RadniPaketi.SelectMany(rp => rp.Zadatci).SelectMany(z => z.Aktivnosti).Count();
                }
            }
        }
    }
}
