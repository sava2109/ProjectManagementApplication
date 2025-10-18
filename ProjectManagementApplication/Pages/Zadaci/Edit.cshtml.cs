using System.Linq;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApplication.Pages.Zadaci
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ZadatakInput Zadatak { get; set; } = new ZadatakInput();

        public SelectList RadniPaketiLista { get; set; } = default!;

        public class ZadatakInput
        {
            public int Id { get; set; }

            [Required]
            public string Naziv { get; set; } = string.Empty;

            public string? Opis { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Planirani sati moraju biti veći od 0")]
            public int PlaniraniSati { get; set; }

            [Required(ErrorMessage = "Morate izabrati radni paket")]
            public int? RadniPaketId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var zadatak = await _context.Zadaci.FindAsync(id);
            if (zadatak == null)
                return NotFound();

            Zadatak = new ZadatakInput
            {
                Id = zadatak.Id,
                Naziv = zadatak.Naziv,
                Opis = zadatak.Opis,
                PlaniraniSati = zadatak.PlaniraniSati,
                RadniPaketId = zadatak.RadniPaketId
            };

            RadniPaketiLista = new SelectList(
                await _context.RadniPaketi.OrderBy(rp => rp.Naziv).ToListAsync(),
                "Id",
                "Naziv",
                Zadatak.RadniPaketId
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RadniPaketiLista = new SelectList(await _context.RadniPaketi.ToListAsync(), "Id", "Naziv");
                return Page();
            }

            var zadatakIzBaze = await _context.Zadaci.FindAsync(Zadatak.Id);
            if (zadatakIzBaze == null)
                return NotFound();

            // Mapiranje iz input modela u entitet
            zadatakIzBaze.Naziv = Zadatak.Naziv;
            zadatakIzBaze.Opis = Zadatak.Opis;
            zadatakIzBaze.PlaniraniSati = Zadatak.PlaniraniSati;
            zadatakIzBaze.RadniPaketId = Zadatak.RadniPaketId!.Value;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
