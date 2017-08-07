using RezhCity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.DAL.EF
{
    public class RezhCityContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AutoParameters> AutoParameters { get; set; }
        public DbSet<RealtyParameters> RealtyParameters { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .HasRequired(c => c.AutoParameters)
                .WithRequiredPrincipal(c => c.Advertisement)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Advertisement>()
                .HasRequired(c => c.RealtyParameters)
                .WithRequiredPrincipal(c => c.Advertisement)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Advertisement>()
                .HasMany(c => c.Images)
                .WithOptional(c => c.Advertisement)
                .HasForeignKey(c => c.AdvertisementId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Advertisement>()
                .HasMany(c => c.Thumbnails)
                .WithOptional(c => c.Advertisement)
                .HasForeignKey(c => c.AdvertisementId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Resume>()
                .HasMany(c => c.Images)
                .WithOptional(c => c.Resume)
                .HasForeignKey(c => c.ResumeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Resume>()
                .HasMany(c => c.Thumbnails)
                .WithOptional(c => c.Resume)
                .HasForeignKey(c => c.ResumeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Resume>()
                .HasMany(c => c.Workplaces)
                .WithRequired(c => c.Resume)
                .HasForeignKey(c => c.ResumeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Resume>()
                .HasMany(c => c.Schools)
                .WithRequired(c => c.Resume)
                .HasForeignKey(c => c.ResumeId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Resume>()
                .HasRequired(c => c.File)
                .WithRequiredPrincipal(c => c.Resume)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public RezhCityContext(string connectionString) : base(connectionString)
        { }

        static RezhCityContext()
        {
            Database.SetInitializer<RezhCityContext>(new RezhCityInitializer());
        }

    }

    class RezhCityInitializer : CreateDatabaseIfNotExists<RezhCityContext>
    {
        protected override void Seed(RezhCityContext context)
        {
            
        }
    }
}
