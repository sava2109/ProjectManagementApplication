using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Aktivnosti
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

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
    }
}
