using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectManagementApplication.Pages.ZaposleniPages
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var zaposleniIzBaze = await _context.Zaposleni.FindAsync(id);
            if (zaposleniIzBaze != null)
            {
                _context.Zaposleni.Remove(zaposleniIzBaze);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
