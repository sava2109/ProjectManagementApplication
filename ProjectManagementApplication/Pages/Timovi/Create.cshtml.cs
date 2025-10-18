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
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TimInput Tim { get; set; } = new TimInput();

        public SelectList ProjektiLista { get; set; } = default!;

        public class TimInput
        {
            [Required(ErrorMessage = "Naziv tima je obavezan")]
            public string Naziv { get; set; } = string.Empty;

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

            var noviTim = new Tim
            {
                Naziv = Tim.Naziv,
                ProjekatId = Tim.ProjekatId!.Value
            };

            _context.Timovi.Add(noviTim);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
