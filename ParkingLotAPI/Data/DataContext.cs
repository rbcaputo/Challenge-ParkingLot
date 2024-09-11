using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Data
{
	public class DataContext : DbContext
	{
		public DbSet<VehicleModel> Vehicles { get; set; }
		public DbSet<FareModel> Fares { get; set; }
		public DbSet<ParkingModel> Parkings { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ParkingModel>()
				.HasOne(p => p.Vehicle)
				.WithMany(v => v.Parkings)
				.HasForeignKey(p => p.VehicleId);

			builder.Entity<ParkingModel>()
				.HasOne(p => p.Fare)
				.WithOne()
				.HasForeignKey<ParkingModel>(p => p.FareId);

			base.OnModelCreating(builder);
		}
	}
}
