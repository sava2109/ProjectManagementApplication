using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.ClanoviTima
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

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
    }
}
