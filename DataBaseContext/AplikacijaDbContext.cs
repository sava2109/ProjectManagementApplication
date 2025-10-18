using DatabaseEntityLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseContext
{
    public class AplikacijaDbContext : DbContext
    {
        public AplikacijaDbContext(DbContextOptions<AplikacijaDbContext> options) : base(options) { }

        // DbSet-ovi za sve entitete
        public DbSet<Projekat> Projekti { get; set; }
        public DbSet<Tim> Timovi { get; set; }
        public DbSet<Zaposleni> Zaposleni { get; set; }
        public DbSet<Aktivnost> Aktivnosti { get; set; }
        public DbSet<DodelaZadatka> DodeleZadataka { get; set; }
        public DbSet<ClanTima> ClanoviTimova { get; set; }

        // Novi DbSet-ovi za RadniPaket i Zadatak
        public DbSet<RadniPaket> RadniPaketi { get; set; }
        public DbSet<Zadatak> Zadaci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracija za Projekat
            modelBuilder.Entity<Projekat>()
                .HasMany(p => p.Timovi)
                .WithOne(t => t.Projekat)
                .HasForeignKey(t => t.ProjekatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Projekat>()
                .HasMany(p => p.RadniPaketi)
                .WithOne(rp => rp.Projekat)
                .HasForeignKey(rp => rp.ProjekatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za RadniPaket
            modelBuilder.Entity<RadniPaket>()
                .HasMany(rp => rp.Zadatci)
                .WithOne(z => z.RadniPaket)
                .HasForeignKey(z => z.RadniPaketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za Tim
            modelBuilder.Entity<Tim>()
                .HasMany(t => t.Clanovi)
                .WithOne(ct => ct.Tim)
                .HasForeignKey(ct => ct.TimId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za ClanTima
            modelBuilder.Entity<ClanTima>()
                .HasOne(ct => ct.Zaposleni)
                .WithMany(z => z.Clanstva)
                .HasForeignKey(ct => ct.ZaposleniId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za Zaposlenog
            modelBuilder.Entity<Zaposleni>()
                .HasMany(z => z.Dodele)
                .WithOne(d => d.Zaposleni)
                .HasForeignKey(d => d.ZaposleniId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za Aktivnost
            modelBuilder.Entity<Aktivnost>()
                .HasMany(a => a.Dodele)
                .WithOne(d => d.Aktivnost)
                .HasForeignKey(d => d.AktivnostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracija za Zadatak
            modelBuilder.Entity<Zadatak>()
                .HasMany(z => z.Aktivnosti)
                .WithOne(a => a.Zadatak)
                .HasForeignKey(a => a.ZadatakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
