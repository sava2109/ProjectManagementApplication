using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.RadniPaketi
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public RadniPaket RadniPaket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            RadniPaket = await _context.RadniPaketi.Include(rp => rp.Projekat)
                                                   .FirstOrDefaultAsync(rp => rp.Id == id);
            if (RadniPaket == null)
                return NotFound();

            return Page();
        }
    }
}
