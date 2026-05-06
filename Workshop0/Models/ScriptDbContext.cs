using Microsoft.EntityFrameworkCore;

namespace Workshop0.Models
{
    // Dans le terminal du gestionnaire de package NuGet
    // Install-Package Microsoft.EntityFrameworkCore.Tools
    // dotnet tool install --global dotnet-ef
    // Dans le dossier du projet cd Workshop0
    // dotnet ef migration add InitialCreate
    // dotnet ef database update
    public class ScriptDbContext(string path="Data/script.db") : DbContext
    {
        public DbSet<Script> Scripts { get; set; }
        public DbSet<ScriptParameter> ScriptParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
            optionsBuilder.UseLazyLoadingProxies(); // Microsoft.EntityFrameworkCore.Proxies package required
        }
    }
}
