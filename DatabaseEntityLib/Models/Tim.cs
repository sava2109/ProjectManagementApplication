using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class Tim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv tima je obavezan")]
        public string Naziv { get; set; } = string.Empty;

        // Veza sa projektom kojem tim pripada
        [Required(ErrorMessage = "Morate izabrati projekat")]
        public int? ProjekatId { get; set; }   // <--- nullable int

        public Projekat Projekat { get; set; } = null!;

        // Članovi tima
        public ICollection<ClanTima> Clanovi { get; set; } = new List<ClanTima>();
    }
}
