using Javsdt.Shared.Constants;
using Javsdt.Shared.Entitys.File;
using Javsdt.Shared.Models.Entitys.Process;
using Javsdt.Shared.Utils.Platform;
using Microsoft.EntityFrameworkCore;

namespace Javsdt.Data.Persistence
{
    public class JavsdtContext : DbContext
    {

        // 单数据库用下面
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={FileEnvironment.JavsdtSqlitePath}")
                .UseLoggerFactory(ApplicationLogging.EfLoggerFactory) // 使用自定义的ILoggerFactory
                .EnableSensitiveDataLogging(); // 可选，启用敏感数据记录
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NasJavFolder>()
            //    .HasOne(folder => folder.Car)
            //    .WithMany()
            //    .HasForeignKey(folder => folder.CarName)
            //    .IsRequired(false);

            //1Jav对1字幕文件
            modelBuilder.Entity<Jav>()
                .HasMany(jav => jav.Subtitles)
                .WithMany(subtitle => subtitle.Javs)
                .UsingEntity("JavSubtitles");

            modelBuilder.Entity<JavCar>()
                .HasMany(jc => jc.Javs);

        }

        public DbSet<Jav> Javs { get; set; }

        public DbSet<Subtitle> Subtitles { get; set; }

        public DbSet<JavCar> JavCars { get; set; }

        public DbSet<NasJavFolder> NasJavFolders { get; set; }

        public void DeleteAllData<T>() where T : class
        {
            DbSet<T> entitySet = Set<T>();
            entitySet.RemoveRange(entitySet);
            SaveChanges();
        }

    }
}
