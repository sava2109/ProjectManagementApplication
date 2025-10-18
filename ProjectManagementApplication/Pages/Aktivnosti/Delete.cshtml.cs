using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Aktivnosti
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Aktivnost Aktivnost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Aktivnost = await _context.Aktivnosti
                .Include(a => a.Zadatak)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Aktivnost == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var aktivnost = await _context.Aktivnosti.FindAsync(id);
            if (aktivnost != null)
            {
                _context.Aktivnosti.Remove(aktivnost);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
