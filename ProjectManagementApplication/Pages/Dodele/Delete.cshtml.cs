using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;

namespace ProjectManagementApplication.Pages.Dodele
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DodelaZadatka Dodela { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Dodela = await _context.DodeleZadataka
                .Include(d => d.Aktivnost)
                .Include(d => d.Zaposleni)
                .FirstOrDefaultAsync(d => d.Id == id) ?? throw new System.Exception("Dodela nije pronađena");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var dodela = await _context.DodeleZadataka.FindAsync(id);
            if (dodela != null)
            {
                _context.DodeleZadataka.Remove(dodela);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
