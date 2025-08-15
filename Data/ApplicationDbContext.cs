using Microsoft.EntityFrameworkCore;
using CariProjesi.Models;

namespace CariProjesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountMovement> AccountMovements { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CariProjesi.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId);

            modelBuilder.Entity<AccountMovement>()
                .HasKey(am => am.MovementId);

            modelBuilder.Entity<AccountMovement>()
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(am => am.AccountId);
        }
    }
}