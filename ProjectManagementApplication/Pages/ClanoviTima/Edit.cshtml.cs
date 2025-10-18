using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApplication.Pages.ClanoviTima
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        // DTO za vezu sa formom
        public class ClanTimaInput
        {
            public int Id { get; set; }
            public int TimId { get; set; }
            public int ZaposleniId { get; set; }
            public string? Uloga { get; set; }
            public DateTime DatumPridruzivanja { get; set; }
        }

        [BindProperty]
        public ClanTimaInput ClanTimaForm { get; set; } = new ClanTimaInput();

        public SelectList TimoviLista { get; set; } = default!;
        public List<Zaposleni> ZaposleniLista { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var clan = await _context.ClanoviTimova
                .Include(c => c.Zaposleni)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clan == null)
                return NotFound();

            // Popuni formu iz baze
            ClanTimaForm = new ClanTimaInput
            {
                Id = clan.Id,
                TimId = clan.TimId,
                ZaposleniId = clan.ZaposleniId,
                Uloga = string.IsNullOrEmpty(clan.Uloga) ? clan.Zaposleni?.Pozicija : clan.Uloga,
                DatumPridruzivanja = clan.DatumPridruzivanja
            };

            TimoviLista = new SelectList(await _context.Timovi.ToListAsync(), "Id", "Naziv", ClanTimaForm.TimId);

            // ZaposleniLista sa Ime + Prezime
            ZaposleniLista = await _context.Zaposleni
                .OrderBy(z => z.Ime)
                .ThenBy(z => z.Prezime)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TimoviLista = new SelectList(await _context.Timovi.ToListAsync(), "Id", "Naziv", ClanTimaForm.TimId);
                ZaposleniLista = await _context.Zaposleni
                    .OrderBy(z => z.Ime)
                    .ThenBy(z => z.Prezime)
                    .ToListAsync();
                return Page();
            }

            var clan = await _context.ClanoviTimova.FindAsync(ClanTimaForm.Id);
            if (clan == null) return NotFound();

            // Ažuriraj entitet
            clan.TimId = ClanTimaForm.TimId;
            clan.ZaposleniId = ClanTimaForm.ZaposleniId;
            clan.Uloga = ClanTimaForm.Uloga;
            clan.DatumPridruzivanja = ClanTimaForm.DatumPridruzivanja;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
