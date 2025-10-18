using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Timovi
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TimInput Tim { get; set; } = new TimInput();

        public SelectList ProjektiLista { get; set; } = default!;

        public class TimInput
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Naziv tima je obavezan")]
            public string Naziv { get; set; } = string.Empty;

            [Required(ErrorMessage = "Morate izabrati projekat")]
            public int? ProjekatId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var timIzBaze = await _context.Timovi.FindAsync(id);
            if (timIzBaze == null)
                return NotFound();

            Tim = new TimInput
            {
                Id = timIzBaze.Id,
                Naziv = timIzBaze.Naziv,
                ProjekatId = timIzBaze.ProjekatId
            };

            ProjektiLista = new SelectList(await _context.Projekti.ToListAsync(), "Id", "Naziv", Tim.ProjekatId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProjektiLista = new SelectList(await _context.Projekti.ToListAsync(), "Id", "Naziv", Tim.ProjekatId);
                return Page();
            }

            var timIzBaze = await _context.Timovi.FindAsync(Tim.Id);
            if (timIzBaze == null)
                return NotFound();

            timIzBaze.Naziv = Tim.Naziv;
            timIzBaze.ProjekatId = Tim.ProjekatId!.Value;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
