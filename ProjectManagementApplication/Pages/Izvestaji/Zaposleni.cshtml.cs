using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Izvestaji
{
    public class ZaposleniModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public ZaposleniModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? ZaposleniId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Godina { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Mesec { get; set; }

        public SelectList ZaposleniSelect { get; set; } = null!;
        public IzvestajZaposleni? Izvestaj { get; set; }

        public async Task OnGetAsync()
        {
            var zaposleni = await _context.Zaposleni
                .Select(z => new { z.Id, ImePrezime = z.Ime + " " + z.Prezime })
                .ToListAsync();

            ZaposleniSelect = new SelectList(zaposleni, "Id", "ImePrezime");

            if (ZaposleniId.HasValue && Godina.HasValue && Mesec.HasValue)
            {
                var startDate = new DateTime(Godina.Value, Mesec.Value, 1);
                var endDate = startDate.AddMonths(1);

                var aktivnosti = await _context.DodeleZadataka
                    .Where(d => d.ZaposleniId == ZaposleniId.Value)
                    .Include(d => d.Aktivnost)
                        .ThenInclude(a => a.Zadatak)
                            .ThenInclude(z => z.RadniPaket)
                                .ThenInclude(rp => rp.Projekat)
                    .Select(d => d.Aktivnost)
                    .Where(a => a.Zadatak.RadniPaket.Projekat.DatumPocetka >= startDate &&
                                a.Zadatak.RadniPaket.Projekat.DatumPocetka < endDate)
                    .ToListAsync();

                Izvestaj = new IzvestajZaposleni
                {
                    Zaposleni = zaposleni.FirstOrDefault(z => z.Id == ZaposleniId.Value)?.ImePrezime ?? "",
                    UkupnoAktivnosti = aktivnosti.Count,
                    Projekti = aktivnosti
                        .GroupBy(a => a.Zadatak.RadniPaket.Projekat)
                        .Select(pg => new IzvestajZaposleni.ProjekatInfo
                        {
                            Naziv = pg.Key.Naziv,
                            DatumPocetka = pg.Key.DatumPocetka,
                            DatumZavrsetka = pg.Key.DatumZavrsetka,
                            RadniPaketi = pg.GroupBy(a => a.Zadatak.RadniPaket)
                                .Select(rpg => new IzvestajZaposleni.RadniPaketInfo
                                {
                                    Naziv = rpg.Key.Naziv,
                                    Zadaci = rpg.GroupBy(a => a.Zadatak)
                                        .Select(zg => new IzvestajZaposleni.ZadatakInfo
                                        {
                                            Naziv = zg.Key.Naziv,
                                            Aktivnosti = zg.Select(a => new IzvestajZaposleni.AktivnostInfo
                                            {
                                                Naziv = a.Naziv,
                                                Status = a.Status
                                            }).ToList()
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                };
            }
        }

        public class IzvestajZaposleni
        {
            public string Zaposleni { get; set; } = "";
            public int UkupnoAktivnosti { get; set; }
            public List<ProjekatInfo> Projekti { get; set; } = new();

            public class ProjekatInfo
            {
                public string Naziv { get; set; } = "";
                public DateTime DatumPocetka { get; set; }
                public DateTime DatumZavrsetka { get; set; }
                public List<RadniPaketInfo> RadniPaketi { get; set; } = new();
            }

            public class RadniPaketInfo
            {
                public string Naziv { get; set; } = "";
                public List<ZadatakInfo> Zadaci { get; set; } = new();
            }

            public class ZadatakInfo
            {
                public string Naziv { get; set; } = "";
                public List<AktivnostInfo> Aktivnosti { get; set; } = new();
            }

            public class AktivnostInfo
            {
                public string Naziv { get; set; } = "";
                public string Status { get; set; } = "";
            }
        }
    }
}
