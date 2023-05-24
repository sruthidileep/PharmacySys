using Microsoft.EntityFrameworkCore;
using PharmacySys.Models;

namespace PharmacySys.Repository
{
    public class PharmacyDbContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server=HPENVY05022022;Database=PharmacySys;User Id=sa;Password=Ready2023;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
    