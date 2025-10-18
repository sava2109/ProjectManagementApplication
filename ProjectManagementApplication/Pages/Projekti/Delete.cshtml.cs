using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DataBaseContext;
using DatabaseEntityLib.Models;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Projekti
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Projekat Projekat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Projekat = await _context.Projekti.FindAsync(id);

            if (Projekat == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Projekat == null)
            {
                return NotFound();
            }

            var projekatZaBrisanje = await _context.Projekti.FindAsync(Projekat.Id);
            if (projekatZaBrisanje != null)
            {
                _context.Projekti.Remove(projekatZaBrisanje);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
