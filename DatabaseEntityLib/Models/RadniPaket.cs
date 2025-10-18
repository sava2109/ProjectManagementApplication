using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class RadniPaket
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; } = string.Empty;

        public string? Opis { get; set; }

        public int PlaniraniSati { get; set; }

        [Required(ErrorMessage = "Morate izabrati projekat")]
        public int? ProjekatId { get; set; }
        public Projekat Projekat { get; set; } = null!;

        public ICollection<Zadatak> Zadatci { get; set; } = new List<Zadatak>();
    }
}
