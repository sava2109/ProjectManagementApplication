using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DataBaseContext;
using DatabaseEntityLib.Models;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Projekti
{
    public class CreateModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public CreateModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Projekat Projekat { get; set; } = new Projekat();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Projekti.Add(Projekat);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
