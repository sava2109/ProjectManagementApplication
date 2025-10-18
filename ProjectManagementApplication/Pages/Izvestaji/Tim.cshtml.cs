using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib.Models;

namespace ProjectManagementApplication.Pages.Izvestaji
{
    public class TimModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public TimModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? TimId { get; set; }

        public SelectList TimSelect { get; set; }

        public List<ClanTima> ClanoviTima { get; set; } = new List<ClanTima>();

        public int UkupnoClanova => ClanoviTima.Count;

        public async Task OnGetAsync()
        {
            TimSelect = new SelectList(await _context.Timovi.ToListAsync(), "Id", "Naziv");

            if (TimId.HasValue)
            {
                ClanoviTima = await _context.ClanoviTimova
                    .Include(c => c.Zaposleni)
                    .Where(c => c.TimId == TimId.Value)
                    .OrderBy(c => c.Zaposleni.Ime)
                    .ToListAsync();
            }
        }
    }
}
