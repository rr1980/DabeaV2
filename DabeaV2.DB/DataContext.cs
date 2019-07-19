using DabeaV2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DabeaV2.DB
{
    public class DataContext : DbContext
    {
        public DbSet<Modification> Modifications { get; set; }
        public DbSet<ModificationItem> ModificationItems { get; set; }

        public DbSet<Person> Personen { get; set; }
        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Kontakt> Kontakte { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //options.UseLazyLoadingProxies();
            ////options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), assembly => assembly.MigrationsAssembly("DB"));
            //options.UseInMemoryDatabase(databaseName: "Rene_KitaTest");

            //options.EnableDetailedErrors();
            ////options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modification>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Benutzer).WithMany(x => x.OwnModifications).HasForeignKey(x => x.BenutzerId);

                entity.HasOne(x => x.ChangedPerson).WithMany(x => x.Modifications).HasForeignKey(x => x.ChangedPersonId);
                entity.HasOne(x => x.ChangedBenutzer).WithMany(x => x.Modifications).HasForeignKey(x => x.ChangedBenutzerId);
                entity.HasOne(x => x.ChangedKontakt).WithMany(x => x.Modifications).HasForeignKey(x => x.ChangedKontaktId);
            });

            modelBuilder.Entity<ModificationItem>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Modification).WithMany(x => x.ModificationItems).HasForeignKey(x => x.ModificationId);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Benutzer>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Person).WithMany(x => x.Benutzer).HasForeignKey(x => x.PersonId);
            });

            modelBuilder.Entity<Kontakt>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Person).WithMany(x => x.Kontakte).HasForeignKey(x => x.PersonId);
            });
        }
    }
}
