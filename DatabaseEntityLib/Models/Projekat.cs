using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseEntityLib.Models
{
    public class Projekat
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; } = string.Empty;

        public string? Opis { get; set; }

        public int PlaniraniSati { get; set; }

        public DateTime DatumPocetka { get; set; }

        public DateTime DatumZavrsetka { get; set; }

        // Projekat ima više radnih paketa
        public ICollection<RadniPaket> RadniPaketi { get; set; } = new List<RadniPaket>();

        // Projekat može imati više timova
        public ICollection<Tim> Timovi { get; set; } = new List<Tim>();
    }
}
