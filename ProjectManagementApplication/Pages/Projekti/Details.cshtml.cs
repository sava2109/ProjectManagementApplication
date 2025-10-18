using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DataBaseContext;
using DatabaseEntityLib.Models;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Projekti
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

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
    }
}
