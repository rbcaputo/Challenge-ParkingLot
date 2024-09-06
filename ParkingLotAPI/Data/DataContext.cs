using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Data
{
	public class DataContext : DbContext
	{
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Pricing> Pricings { get; set; }
		public DbSet<Parking> Parkings { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Parking>()
				.HasMany(p => p.Vehicles)
				.WithOne()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Parking>()
				.HasOne(p => p.Pricing)
				.WithMany()
				.HasForeignKey(p => p.PricingId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
