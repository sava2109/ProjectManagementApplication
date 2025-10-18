using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class Aktivnost
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; } = string.Empty;

        public string? Opis { get; set; }
        public int PlaniraniSati { get; set; }
        public string Status { get; set; } = string.Empty;

        public int ZadatakId { get; set; }
        public Zadatak Zadatak { get; set; } = null!;

        public ICollection<DodelaZadatka> Dodele { get; set; } = new List<DodelaZadatka>();
    }
}
