namespace DatabaseEntityLib.Models
{
    public class TimZaposleni
    {
        public int TimId { get; set; }
        public Tim Tim { get; set; }

        public int ZaposleniId { get; set; }
        public Zaposleni Zaposleni { get; set; }
    }
}
