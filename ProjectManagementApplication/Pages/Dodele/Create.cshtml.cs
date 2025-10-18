using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Dodele
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DodelaInput DodelaForm { get; set; } = new DodelaInput();

        public SelectList AktivnostiLista { get; set; } = default!;
        public List<Zaposleni> ZaposleniLista { get; set; } = default!;

        public class DodelaInput
        {
            public int AktivnostId { get; set; }
            public int ZaposleniId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AktivnostiLista = new SelectList(await _context.Aktivnosti.ToListAsync(), "Id", "Naziv");
            ZaposleniLista = await _context.Zaposleni.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AktivnostiLista = new SelectList(await _context.Aktivnosti.ToListAsync(), "Id", "Naziv");
                ZaposleniLista = await _context.Zaposleni.ToListAsync();
                return Page();
            }

            // Kreiramo novu dodelu
            var novaDodela = new DodelaZadatka
            {
                AktivnostId = DodelaForm.AktivnostId,
                ZaposleniId = DodelaForm.ZaposleniId
            };

            _context.DodeleZadataka.Add(novaDodela);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
