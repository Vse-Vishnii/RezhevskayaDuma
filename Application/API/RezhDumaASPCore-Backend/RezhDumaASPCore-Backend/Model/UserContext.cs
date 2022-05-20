using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using RezhDumaASPCore_Backend.Controllers;

namespace RezhDumaASPCore_Backend.Model
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<CategoryApplication> CategoryApplications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DistrictApplication> DistrictApplications { get; set; }
        public DbSet<DeputyApplication> DeputyApplications { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            Default.CreateData(this);
        }

        public TEntity PullEntity<TEntity>(string id)
        where TEntity : DbEntity
        {
            return Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> PullCollection<TEntity, TConnection>(Application app)
            where TEntity : DbEntity
            where TConnection : ApplicationConnection<TEntity>
        {
            return Set<TConnection>()
                .Where(ca => ca.ApplicationId.Equals(app.Id))
                .Select(ca => ca.ConnectedEntity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Filename=DB\\rezhdb.db;Foreign Keys=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Answer)
                .WithOne(a => a.Application)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Applicant)
                .WithMany(a => a.Applications)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<User>().HasOne(u => u.District).WithOne(d => d.Deputy);
            //modelBuilder.Entity<User>().HasOne(u => u.Category).WithOne(c => c.Deputy);
            CreateCategoryApplication(modelBuilder);
            CreateDistrictApplication(modelBuilder);
            CreateDeputyApplication(modelBuilder);
            CreateDeputyConnections(modelBuilder);
        }

        private static void CreateDeputyConnections(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(d => d.District).WithOne(d => d.Deputy)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasOne(d => d.Category).WithOne(c => c.Deputy)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void CreateDeputyApplication(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.DeputyApplication)
                .WithOne(d => d.Application);
            modelBuilder.Entity<User>()
                .HasMany(a => a.DeputyApplications)
                .WithOne(d => d.Deputy).OnDelete(DeleteBehavior.NoAction);
        }

        private static void CreateDistrictApplication(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(a => a.Districts)
                .WithMany(d => d.Applications)
                .UsingEntity<DistrictApplication>(
                    c => c.HasOne(c => c.ConnectedEntity)
                        .WithMany(c => c.DistrictApplications)
                        .HasForeignKey(c => c.ConnectedEntityId).OnDelete(DeleteBehavior.Cascade),
                    c => c.HasOne(c => c.Application)
                        .WithMany(c => c.DistrictApplications)
                        .HasForeignKey(c => c.ApplicationId).OnDelete(DeleteBehavior.Cascade));
        }

        private static void CreateCategoryApplication(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(a => a.Categories)
                .WithMany(c => c.Applications)
                .UsingEntity<CategoryApplication>(
                    c => c
                        .HasOne(c => c.ConnectedEntity)
                        .WithMany(c => c.CategoryApplications)
                        .HasForeignKey(c => c.ConnectedEntityId).OnDelete(DeleteBehavior.Cascade),
                    c => c
                        .HasOne(c => c.Application)
                        .WithMany(c => c.CategoryApplications)
                        .HasForeignKey(c => c.ApplicationId).OnDelete(DeleteBehavior.Cascade));
        }
    }
}
