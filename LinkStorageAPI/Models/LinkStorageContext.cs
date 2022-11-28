using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace LinkStorageAPI.Models
{
    public class LinkStorageContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public LinkStorageContext(DbContextOptions<LinkStorageContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("LinkStorage");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Lists> Lists { get; set; } = null!;
        public DbSet<Links> Links { get; set; } = null!;
    }
}
