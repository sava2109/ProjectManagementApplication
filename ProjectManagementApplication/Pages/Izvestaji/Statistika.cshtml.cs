using DataBaseContext;
using DatabaseEntityLib.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Pages.Izvestaji
{
    public class StatistikaModel : PageModel
    {
        private readonly AplikacijaDbContext _context;

        public StatistikaModel(AplikacijaDbContext context)
        {
            _context = context;
        }

        // Ukupni brojevi
        public int UkupnoProjekata { get; set; }
        public int UkupnoZaposlenih { get; set; }
        public int UkupnoDodela { get; set; }

        // Podaci za grafikone
        public List<BarChartData> ZadaciPoZaposlenom { get; set; } = new();
        public List<PieChartData> ZadaciPoStatusu { get; set; } = new();

        public async Task OnGetAsync()
        {
            UkupnoProjekata = await _context.Projekti.CountAsync();
            UkupnoZaposlenih = await _context.Zaposleni.CountAsync();
            UkupnoDodela = await _context.DodeleZadataka.CountAsync();

            // Bar chart: broj zadataka po zaposlenom
            ZadaciPoZaposlenom = await _context.Zaposleni
                .Select(z => new BarChartData
                {
                    Label = z.Ime + " " + z.Prezime,
                    Value = z.Dodele.Select(d => d.AktivnostId).Distinct().Count()
                })
                .ToListAsync();

            // Pie chart: procentualna raspodela zadataka po statusima
            ZadaciPoStatusu = await _context.Aktivnosti
                .GroupBy(a => a.Status)
                .Select(g => new PieChartData
                {
                    Label = g.Key,
                    Value = g.Count()
                })
                .ToListAsync();
        }

        public class BarChartData
        {
            public string Label { get; set; } = string.Empty;
            public int Value { get; set; }
        }

        public class PieChartData
        {
            public string Label { get; set; } = string.Empty;
            public int Value { get; set; }
        }
    }
}
