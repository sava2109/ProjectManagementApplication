using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Aktivnosti
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AktivnostInput Aktivnost { get; set; } = new AktivnostInput();

        public SelectList ZadaciLista { get; set; } = default!;
        public SelectList StatusLista { get; set; } = default!;

        public class AktivnostInput
        {
            public int Id { get; set; }

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var aktivnostIzBaze = await _context.Aktivnosti.FindAsync(id);
            if (aktivnostIzBaze == null)
                return NotFound();

            Aktivnost = new AktivnostInput
            {
                Id = aktivnostIzBaze.Id,
                Naziv = aktivnostIzBaze.Naziv,
                Opis = aktivnostIzBaze.Opis,
                PlaniraniSati = aktivnostIzBaze.PlaniraniSati,
                ZadatakId = aktivnostIzBaze.ZadatakId,
                Status = aktivnostIzBaze.Status
            };

            ZadaciLista = new SelectList(await _context.Zadaci.ToListAsync(), "Id", "Naziv", Aktivnost.ZadatakId);
            StatusLista = new SelectList(new[] { "U toku", "Završeno", "Kašnjenje", "Planirano" }, Aktivnost.Status);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ZadaciLista = new SelectList(await _context.Zadaci.ToListAsync(), "Id", "Naziv", Aktivnost.ZadatakId);
                StatusLista = new SelectList(new[] { "U toku", "Završeno", "Kašnjenje", "Planirano" }, Aktivnost.Status);
                return Page();
            }

            var aktivnostIzBaze = await _context.Aktivnosti.FindAsync(Aktivnost.Id);
            if (aktivnostIzBaze == null)
                return NotFound();

            // Mapiranje iz input modela u entitet
            aktivnostIzBaze.Naziv = Aktivnost.Naziv;
            aktivnostIzBaze.Opis = Aktivnost.Opis;
            aktivnostIzBaze.PlaniraniSati = Aktivnost.PlaniraniSati;
            aktivnostIzBaze.ZadatakId = Aktivnost.ZadatakId!.Value;
            aktivnostIzBaze.Status = Aktivnost.Status;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
