using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Projekti
{
    public class EditModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public EditModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Projekat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projekti.Any(p => p.Id == Projekat.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}
