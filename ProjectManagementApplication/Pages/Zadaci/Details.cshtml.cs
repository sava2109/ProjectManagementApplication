using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Zadaci
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public Zadatak Zadatak { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Zadatak = await _context.Zadaci
                .Include(z => z.RadniPaket)
                .FirstOrDefaultAsync(z => z.Id == id);

            if (Zadatak == null)
                return NotFound();

            return Page();
        }
    }
}
