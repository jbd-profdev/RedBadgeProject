using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RedBadgeProject.Server.Models;

namespace RedBadgeProject.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<StaffEntity> Staff { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocationEntity>().HasData(
                new LocationEntity
                {
                    Id = 1,
                    Name = "Indianapolis"
                }
                , new LocationEntity
                {
                    Id = 2,
                    Name = "Phoenix"
                }
            );

            modelBuilder.Entity<CompanyEntity>().HasData(
                new CompanyEntity
                {
                    Id = 1,
                    Name = "BusCo",
                    LocationId = 2
                }
                , new CompanyEntity
                {
                    Id = 2,
                    Name = "BoatCo",
                    LocationId = 1
                }
            );

            modelBuilder.Entity<VehicleEntity>().HasData(
                new VehicleEntity
                {
                    Id = 1,
                    Name = "Boston Whaler",
                    CompanyId = 2,
                    Capacity = 5
                }
                , new VehicleEntity
                {
                    Id = 2,
                    Name = "International Schooler",
                    CompanyId = 1,
                    Capacity = 30
                }
            );

            modelBuilder.Entity<StaffEntity>().HasData(
                new StaffEntity
                {
                    Id = 1,
                    Name = "Joe",
                    CompanyId = 1,
                    CurrentLocationId = 2,
                    RoleId = 0
                }
                , new StaffEntity
                {
                    Id = 2,
                    Name = "Jane",
                    CompanyId = 2,
                    CurrentLocationId = 1,
                    RoleId = 0
                }
            );

            modelBuilder.Entity<TripEntity>().HasData(
                new TripEntity
                {
                    Id = 1,
                    Name = "Indy to Phoenix",
                    LocationFromId = 1,
                    LocationToId = 2,
                    VehicleId = 1,
                    StartDate = DateTimeOffset.UtcNow,
                    EndDate = DateTimeOffset.UtcNow.AddDays(2),
                    Capacity = 26
                }
                , new TripEntity
                {
                    Id = 2,
                    Name = "Phoenix to Indy",
                    LocationFromId = 2,
                    LocationToId = 1,
                    VehicleId = 2,
                    StartDate = DateTimeOffset.UtcNow,
                    EndDate = DateTimeOffset.UtcNow.AddDays(14),
                    Capacity = 4
                }
            );

        }
    }
}