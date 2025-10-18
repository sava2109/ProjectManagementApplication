using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Aktivnosti
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AktivnostInput Aktivnost { get; set; } = new AktivnostInput();

        // Drop-down lista za zadatke
        public SelectList ZadaciLista { get; set; } = default!;

        // Drop-down lista za status
        public SelectList StatusLista { get; set; } = default!;

        public class AktivnostInput
        {
            [Required]
            public string Naziv { get; set; } = string.Empty;

            public string? Opis { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Planirani sati moraju biti veći od 0")]
            public int PlaniraniSati { get; set; }

            [Required(ErrorMessage = "Morate izabrati zadatak")]
            public int? ZadatakId { get; set; }

            [Required(ErrorMessage = "Morate izabrati status")]
            public string Status { get; set; } = "Planirano";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ZadaciLista = new SelectList(await _context.Zadaci.ToListAsync(), "Id", "Naziv");
            StatusLista = new SelectList(new[] { "U toku", "Završeno", "Kašnjenje", "Planirano" });
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ZadaciLista = new SelectList(await _context.Zadaci.ToListAsync(), "Id", "Naziv");
                StatusLista = new SelectList(new[] { "U toku", "Završeno", "Kašnjenje", "Planirano" });
                return Page();
            }

            var novaAktivnost = new Aktivnost
            {
                Naziv = Aktivnost.Naziv,
                Opis = Aktivnost.Opis,
                PlaniraniSati = Aktivnost.PlaniraniSati,
                ZadatakId = Aktivnost.ZadatakId!.Value,
                Status = Aktivnost.Status
            };

            _context.Aktivnosti.Add(novaAktivnost);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
