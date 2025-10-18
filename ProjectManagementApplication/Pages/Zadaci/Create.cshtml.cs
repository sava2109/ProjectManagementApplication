using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Zadaci
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ZadatakInput Zadatak { get; set; } = new ZadatakInput();

        // Drop-down lista za radne pakete
        public SelectList RadniPaketiLista { get; set; } = default!;

        public class ZadatakInput
        {
            [Required]
            public string Naziv { get; set; } = string.Empty;

            public string? Opis { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Planirani sati moraju biti veći od 0")]
            public int PlaniraniSati { get; set; }

            [Required(ErrorMessage = "Morate izabrati radni paket")]
            public int? RadniPaketId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RadniPaketiLista = new SelectList(await _context.RadniPaketi.ToListAsync(), "Id", "Naziv");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RadniPaketiLista = new SelectList(await _context.RadniPaketi.ToListAsync(), "Id", "Naziv");
                return Page();
            }

            var noviZadatak = new Zadatak
            {
                Naziv = Zadatak.Naziv,
                Opis = Zadatak.Opis,
                PlaniraniSati = Zadatak.PlaniraniSati,
                RadniPaketId = Zadatak.RadniPaketId!.Value
            };

            _context.Zadaci.Add(noviZadatak);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
