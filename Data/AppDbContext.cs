using DotNetEnv;
using HRprojekat.Models;
using Microsoft.EntityFrameworkCore;

namespace HRprojekat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Env.Load();

                var host = Environment.GetEnvironmentVariable("DB_HOST");
                var port = Environment.GetEnvironmentVariable("DB_PORT");
                var name = Environment.GetEnvironmentVariable("DB_NAME");
                var user = Environment.GetEnvironmentVariable("DB_USER");
                var pass = Environment.GetEnvironmentVariable("DB_PASS");

                var connectionString = $"server={host};port={port};database={name};user={user};password={pass}";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Skill>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.Candidates)
                .UsingEntity(j => j.ToTable("CandidateSkills"));
        }
    }
}
