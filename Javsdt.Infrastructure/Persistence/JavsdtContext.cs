using Javsdt.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Javsdt.Infrastructure.Persistence
{
    public class JavsdtContext : DbContext
    {
        public JavsdtContext(DbContextOptions<JavsdtContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbContextUtil.GetConnectString()}");
            }
        }

        public JavsdtContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1 Jav 对 1 字幕文件
            modelBuilder.Entity<Jav>()
                .HasMany(jav => jav.Subtitles)
                .WithMany(subtitle => subtitle.Javs)
                .UsingEntity("JavSubtitles");
        }

        public void DeleteAllData<T>() where T : class
        {
            DbSet<T> entitySet = Set<T>();
            entitySet.RemoveRange(entitySet);
            SaveChanges();
        }

        public DbSet<Jav> Javs { get; set; }

        public DbSet<Subtitle> Subtitles { get; set; }
    }
}
