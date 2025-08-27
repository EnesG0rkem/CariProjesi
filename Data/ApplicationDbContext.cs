using Microsoft.EntityFrameworkCore;
using CariProjesi.Models;

namespace CariProjesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Movement> Movements { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use absolute path to ensure runtime and tooling use the same database file
            optionsBuilder.UseSqlite("Data Source=/Users/enesgorkem/Staj/CariProje/CariProje.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountCode);

            modelBuilder.Entity<Movement>()
                .HasKey(am => am.MovementId);

            modelBuilder.Entity<Movement>()
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(am => am.AccountCode);
        }
    }
}