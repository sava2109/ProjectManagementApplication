using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectManagementApplication.Pages.ZaposleniPages
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public Zaposleni Zaposleni { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Zaposleni = await _context.Zaposleni.FindAsync(id);
            if (Zaposleni == null)
                return NotFound();

            return Page();
        }
    }
}
