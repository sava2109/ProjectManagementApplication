using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.RadniPaketi
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RadniPaketInput RadniPaket { get; set; } = new RadniPaketInput();

        // Drop-down lista za projekte
        public SelectList ProjektiLista { get; set; } = default!;

        public class RadniPaketInput
        {
            [Required]
            public string Naziv { get; set; } = string.Empty;

            public string? Opis { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Planirani sati moraju biti veći od 0")]
            public int PlaniraniSati { get; set; }

            [Required(ErrorMessage = "Morate izabrati projekat")]
            public int? ProjekatId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ProjektiLista = new SelectList(await _context.Projekti.ToListAsync(), "Id", "Naziv");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProjektiLista = new SelectList(await _context.Projekti.ToListAsync(), "Id", "Naziv");
                return Page();
            }

            var noviRadniPaket = new RadniPaket
            {
                Naziv = RadniPaket.Naziv,
                Opis = RadniPaket.Opis,
                PlaniraniSati = RadniPaket.PlaniraniSati,
                ProjekatId = RadniPaket.ProjekatId!.Value
            };

            _context.RadniPaketi.Add(noviRadniPaket);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
