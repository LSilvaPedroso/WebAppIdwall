using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Models;

namespace WebAppIdwall.Connections
{
    public class SqlContext : DbContext
    {
        public DbSet<CautionModel> Caution { get; set; }
        public DbSet<CrimesModel> Crimes { get; set; }
        public DbSet<CrimeWantedModel> CrimeWanted { get; set; }
        public DbSet<WantedModel> Wanted { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrimeWantedModel>()
                    .HasKey(cw => new { cw.IdCrime, cw.IdWanted });

            modelBuilder.Entity<CrimeWantedModel>()
                .HasOne(cw => cw.Wanted)
                .WithMany(w => w.CrimeWanted)
                .HasForeignKey(cw => cw.IdWanted);

            modelBuilder.Entity<CrimeWantedModel>()
                .HasOne(cw => cw.Crimes)
                .WithMany(c => c.CrimeWanted)
                .HasForeignKey(cw => cw.IdCrime);


            modelBuilder.Entity<BirthWantedModel>()
                .HasKey(cw => new { cw.IdBirth, cw.IdWanted });

            modelBuilder.Entity<BirthWantedModel>()
                .HasOne(cw => cw.Wanted)
                .WithMany(w => w.BirthWanted)
                .HasForeignKey(cw => cw.IdWanted);

            modelBuilder.Entity<BirthWantedModel>()
                .HasOne(cw => cw.Birth)
                .WithMany(c => c.BirthWanted)
                .HasForeignKey(cw => cw.IdBirth);


            modelBuilder.Entity<CautionModel>()
                    .HasKey(cw => new { cw.IdWanted });
            
            modelBuilder.Entity<CautionModel>()
                .HasOne(cw => cw.Wanted)
                .WithMany(w => w.Cautions)
                .HasForeignKey(fk => fk.IdWanted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
