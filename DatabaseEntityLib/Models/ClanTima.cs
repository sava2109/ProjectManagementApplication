using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class ClanTima
    {
        public int Id { get; set; }

        [Required]
        public int TimId { get; set; }
        public Tim Tim { get; set; } = null!;

        [Required]
        public int ZaposleniId { get; set; }
        public Zaposleni Zaposleni { get; set; } = null!;

        // Opcionalna dodatna polja
        public string? Uloga { get; set; }
        public DateTime DatumPridruzivanja { get; set; } = DateTime.Now;
    }
}
