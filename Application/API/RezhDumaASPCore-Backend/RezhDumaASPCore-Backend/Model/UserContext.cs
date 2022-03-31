using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace RezhDumaASPCore_Backend.Model
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<CategoryApplication> Categories { get; set; }
        public DbSet<DistrictApplication> DistrictApplications { get; set; }
        public UserContext()
        {
            Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=C:\\Documents\\Programming\\RezhevskayaDuma\\RezhevskayaDuma\\Application\\API\\RezhDumaASPCore-Backend\\RezhDumaASPCore-Backend\\DB\\rezhdb.db");
        }
    }
}
