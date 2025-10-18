using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;

namespace ProjectManagementApplication.Pages.Dodele
{
    public class DetailsModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public DetailsModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        public DodelaZadatka Dodela { get; set; } = default!;

        public async Task OnGetAsync(int id)
        {
            Dodela = await _context.DodeleZadataka
                .Include(d => d.Aktivnost)
                .Include(d => d.Zaposleni)
                .FirstOrDefaultAsync(d => d.Id == id) ?? throw new System.Exception("Dodela nije pronađena");
        }
    }
}
