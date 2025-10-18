using Microsoft.AspNetCore.Mvc.RazorPages;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.RadniPaketi
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RadniPaket RadniPaket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            RadniPaket = await _context.RadniPaketi.Include(rp => rp.Projekat)
                                                   .FirstOrDefaultAsync(rp => rp.Id == id);
            if (RadniPaket == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var paket = await _context.RadniPaketi.FindAsync(RadniPaket.Id);
            if (paket != null)
            {
                _context.RadniPaketi.Remove(paket);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
