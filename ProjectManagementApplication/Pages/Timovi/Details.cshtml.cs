using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Timovi
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public Tim Tim { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tim = await _context.Timovi
                                .Include(t => t.Projekat)
                                .FirstOrDefaultAsync(t => t.Id == id);

            if (Tim == null)
                return NotFound();

            return Page();
        }
    }
}
