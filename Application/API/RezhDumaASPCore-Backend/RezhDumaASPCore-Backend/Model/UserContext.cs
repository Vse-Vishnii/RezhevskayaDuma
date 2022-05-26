using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using RezhDumaASPCore_Backend.Controllers;
using RezhDumaASPCore_Backend.Helpers;

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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //Default.CreateData(this);
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
            modelBuilder.CreateApplicationAnswer().CreateUserApplication().CreateCategoryApplication()
                .CreateDistrictApplication().CreateDeputyApplication().CreateDeputyConnections();
        }
    }
}
