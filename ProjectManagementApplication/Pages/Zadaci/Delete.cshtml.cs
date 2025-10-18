using System.Threading.Tasks;
using DatabaseEntityLib.Models;
using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApplication.Pages.Zadaci
{
    public class DeleteModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DeleteModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zadatak Zadatak { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Zadatak = await _context.Zadaci.FirstOrDefaultAsync(z => z.Id == id);

            if (Zadatak == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var zadatak = await _context.Zadaci.FindAsync(Zadatak.Id);

            if (zadatak != null)
            {
                _context.Zadaci.Remove(zadatak);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
