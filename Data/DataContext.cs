using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SistemaLanHouseBackEnd.Models;

namespace SistemaLanHouseBackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            InitializeComputers();
        }

        public DbSet<ComputerModel> Computers { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RegisterModel> Registers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured)
           {
               // Configure the in-memory database
                optionsBuilder.UseInMemoryDatabase("LanHouseDB");
            }
        }

        private void InitializeComputers()
        {
            if (!Computers.Any())
            {
                Computers.AddRange(new[]
                {
                    new ComputerModel { Id = 1, MachineName = "PC1", IsItBeingUsed = false },
                    new ComputerModel { Id = 2, MachineName = "PC2", IsItBeingUsed = false },
                    new ComputerModel { Id = 3, MachineName = "PC3", IsItBeingUsed = false },
                    new ComputerModel { Id = 4, MachineName = "PC4", IsItBeingUsed = false },
                    new ComputerModel { Id = 5, MachineName = "PC5", IsItBeingUsed = false },
                    new ComputerModel { Id = 6, MachineName = "PC6", IsItBeingUsed = false },
                    new ComputerModel { Id = 7, MachineName = "PC7", IsItBeingUsed = false },
                    new ComputerModel { Id = 8, MachineName = "PC8", IsItBeingUsed = false },
                    new ComputerModel { Id = 9, MachineName = "PC9", IsItBeingUsed = false },
                    new ComputerModel { Id = 10, MachineName = "PC10", IsItBeingUsed = false },
                });
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComputerModel>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<RegisterModel>()
               .HasKey(r => r.Id);

            modelBuilder.Entity<RegisterModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registers)
                .HasForeignKey(r => r.UserId)
                .HasPrincipalKey(u => u.Id);

            modelBuilder.Entity<RegisterModel>()
                .HasOne(r => r.Computer)
                .WithMany(c => c.Registers)
                .HasForeignKey(r => r.ComputerId)
                .HasPrincipalKey(c => c.Id);


        }

    }
}
