using AmiFlota.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmiFlota.Data
{
    public class AmiFlotaContext : IdentityDbContext<ApplicationUserModel>
    {
        public AmiFlotaContext(DbContextOptions<AmiFlotaContext> options)
            : base(options)
        {
        }

        public DbSet<CarModel> Cars { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<TripModel> Trips { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

              modelBuilder.Entity<BookingModel>()
                .HasOne(c => c.CarModels)
                .WithMany(b => b.Bookings)
                .HasForeignKey(v => v.CarVIN);

           modelBuilder.Entity<BookingModel>()
                .HasOne(c => c.ApplicationUserModels)
                .WithMany(t => t.BookingModels)
                .HasForeignKey(x => x.UserId);

        }
    }
}
