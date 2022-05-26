using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Helpers
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder CreateApplicationAnswer(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Answer)
                .WithOne(a => a.Application)
                .OnDelete(DeleteBehavior.Cascade);
            return modelBuilder;
        }

        public static ModelBuilder CreateUserApplication(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Applicant)
                .WithMany(a => a.Applications)
                .OnDelete(DeleteBehavior.Cascade);
            return modelBuilder;
        }

        public static ModelBuilder CreateDeputyConnections(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(d => d.District).WithOne(d => d.Deputy)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasOne(d => d.Category).WithOne(c => c.Deputy)
                .OnDelete(DeleteBehavior.NoAction);
            return modelBuilder;
        }

        public static ModelBuilder CreateDeputyApplication(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.DeputyApplication)
                .WithOne(d => d.Application);
            modelBuilder.Entity<User>()
                .HasMany(a => a.DeputyApplications)
                .WithOne(d => d.Deputy).OnDelete(DeleteBehavior.NoAction);
            return modelBuilder;
        }

        public static ModelBuilder CreateDistrictApplication(this ModelBuilder modelBuilder)
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
            return modelBuilder;
        }

        public static ModelBuilder CreateCategoryApplication(this ModelBuilder modelBuilder)
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
            return modelBuilder;
        }
    }
}
