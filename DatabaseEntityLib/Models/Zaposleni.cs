using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class Zaposleni
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; } = string.Empty;

        [Required]
        public string Prezime { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Pozicija { get; set; }

        // Veza sa dodelama zadataka
        public ICollection<DodelaZadatka> Dodele { get; set; } = new List<DodelaZadatka>();

        // Članstva zaposlenog u različitim timovima
        public ICollection<ClanTima> Clanstva { get; set; } = new List<ClanTima>();
    }
}
