using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class Zadatak
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; } = string.Empty;

        public string? Opis { get; set; }
        public int PlaniraniSati { get; set; }

        public int RadniPaketId { get; set; }
        public RadniPaket RadniPaket { get; set; } = null!;

        public ICollection<Aktivnost> Aktivnosti { get; set; } = new List<Aktivnost>();
    }
}
