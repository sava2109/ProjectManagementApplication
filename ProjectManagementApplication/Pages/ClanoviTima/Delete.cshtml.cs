using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.ClanoviTima
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClanTima ClanTima { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ClanTima = await _context.ClanoviTimova
                .Include(c => c.Tim)
                .Include(c => c.Zaposleni)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (ClanTima == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var clan = await _context.ClanoviTimova.FindAsync(id);
            if (clan != null)
            {
                _context.ClanoviTimova.Remove(clan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
