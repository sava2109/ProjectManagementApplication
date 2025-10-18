namespace DatabaseEntityLib.Models
{
    public class DodelaZadatka
    {
        public int Id { get; set; }

        public int ZaposleniId { get; set; }
        public Zaposleni Zaposleni { get; set; } = null!;

        public int AktivnostId { get; set; }
        public Aktivnost Aktivnost { get; set; } = null!;
    }
}
