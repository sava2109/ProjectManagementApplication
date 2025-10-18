using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace ProjectManagementApplication.Pages.ClanoviTima
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClanTimaInput ClanTima { get; set; } = new ClanTimaInput();

        public SelectList TimoviLista { get; set; } = default!;
        public List<Zaposleni> ZaposleniLista { get; set; } = default!;

        public class ClanTimaInput
        {
            public int TimId { get; set; }
            public int ZaposleniId { get; set; }
            public string? Uloga { get; set; }
            public DateTime DatumPridruzivanja { get; set; } = DateTime.Now;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            TimoviLista = new SelectList(await _context.Timovi.ToListAsync(), "Id", "Naziv");
            ZaposleniLista = await _context.Zaposleni.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TimoviLista = new SelectList(await _context.Timovi.ToListAsync(), "Id", "Naziv");
                ZaposleniLista = await _context.Zaposleni.ToListAsync();
                return Page();
            }

            var zaposleni = await _context.Zaposleni.FindAsync(ClanTima.ZaposleniId);
            var clan = new ClanTima
            {
                TimId = ClanTima.TimId,
                ZaposleniId = ClanTima.ZaposleniId,
                Uloga = string.IsNullOrEmpty(ClanTima.Uloga) ? (zaposleni?.Pozicija ?? "") : ClanTima.Uloga,
                DatumPridruzivanja = ClanTima.DatumPridruzivanja
            };

            _context.ClanoviTimova.Add(clan);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
