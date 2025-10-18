using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.ZaposleniPages
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zaposleni Zaposleni { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Zaposleni = await _context.Zaposleni.FindAsync(id);
            if (Zaposleni == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var zaposleniIzBaze = await _context.Zaposleni.FindAsync(Zaposleni.Id);
            if (zaposleniIzBaze == null)
                return NotFound();

            // Update polja
            zaposleniIzBaze.Ime = Zaposleni.Ime;
            zaposleniIzBaze.Prezime = Zaposleni.Prezime;
            zaposleniIzBaze.Email = Zaposleni.Email;
            zaposleniIzBaze.Pozicija = Zaposleni.Pozicija;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
