using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectManagementApplication.Pages.RadniPaketi
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RadniPaketInput RadniPaket { get; set; } = new RadniPaketInput();

        public SelectList ProjektiSelectList { get; set; } = default!;

        public class RadniPaketInput
        {
            public int Id { get; set; }

            [Required]
            public string Naziv { get; set; } = string.Empty;

            public string? Opis { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Planirani sati moraju biti veći od 0")]
            public int PlaniraniSati { get; set; }

            [Required(ErrorMessage = "Morate izabrati projekat")]
            public int? ProjekatId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var radniPaket = await _context.RadniPaketi.FindAsync(id);
            if (radniPaket == null)
                return NotFound();

            // Popuni input model
            RadniPaket = new RadniPaketInput
            {
                Id = radniPaket.Id,
                Naziv = radniPaket.Naziv,
                Opis = radniPaket.Opis,
                PlaniraniSati = radniPaket.PlaniraniSati,
                ProjekatId = radniPaket.ProjekatId
            };

            ProjektiSelectList = new SelectList(await _context.Projekti.OrderBy(p => p.Naziv).ToListAsync(), "Id", "Naziv");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProjektiSelectList = new SelectList(await _context.Projekti.OrderBy(p => p.Naziv).ToListAsync(), "Id", "Naziv");
                return Page();
            }

            var radniPaketIzBaze = await _context.RadniPaketi.FindAsync(RadniPaket.Id);
            if (radniPaketIzBaze == null)
                return NotFound();

            // Mapiramo izmene
            radniPaketIzBaze.Naziv = RadniPaket.Naziv;
            radniPaketIzBaze.Opis = RadniPaket.Opis;
            radniPaketIzBaze.PlaniraniSati = RadniPaket.PlaniraniSati;
            radniPaketIzBaze.ProjekatId = RadniPaket.ProjekatId!.Value;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
