using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Dodele
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        // DTO za formu
        public class DodelaInput
        {
            public int Id { get; set; }
            public int AktivnostId { get; set; }
            public int ZaposleniId { get; set; }
        }

        [BindProperty]
        public DodelaInput DodelaForm { get; set; } = new DodelaInput();

        public SelectList AktivnostiLista { get; set; } = default!;
        public List<Zaposleni> ZaposleniLista { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var dodela = await _context.DodeleZadataka.FindAsync(id);
            if (dodela == null) return NotFound();

            DodelaForm = new DodelaInput
            {
                Id = dodela.Id,
                AktivnostId = dodela.AktivnostId,
                ZaposleniId = dodela.ZaposleniId
            };

            AktivnostiLista = new SelectList(await _context.Aktivnosti.ToListAsync(), "Id", "Naziv", DodelaForm.AktivnostId);
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
                AktivnostiLista = new SelectList(await _context.Aktivnosti.ToListAsync(), "Id", "Naziv", DodelaForm.AktivnostId);
                ZaposleniLista = await _context.Zaposleni
                    .OrderBy(z => z.Ime)
                    .ThenBy(z => z.Prezime)
                    .ToListAsync();
                return Page();
            }

            var dodela = await _context.DodeleZadataka.FindAsync(DodelaForm.Id);
            if (dodela == null) return NotFound();

            // Ažuriraj entitet
            dodela.AktivnostId = DodelaForm.AktivnostId;
            dodela.ZaposleniId = DodelaForm.ZaposleniId;

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
