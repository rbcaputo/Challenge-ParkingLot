using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Data
{
	public class DataContext : DbContext
	{
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Fare> Fares { get; set; }
		public DbSet<Parking> Parkings { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Parking>()
				.HasOne(p => p.Vehicle)
				.WithMany(v => v.Parkings)
				.HasForeignKey(p => p.VehicleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Parking>()
				.HasOne(p => p.Fare)
				.WithMany(f => f.Parkings)
				.HasForeignKey(p => p.FareId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}
	}
}
