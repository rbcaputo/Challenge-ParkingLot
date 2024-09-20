using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Data
{
	public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
	{
		public DbSet<VehicleModel> Vehicles { get; set; }
		public DbSet<FareModel> Fares { get; set; }
		public DbSet<ParkingModel> Parkings { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ParkingModel>()
				.HasOne(p => p.Vehicle)
				.WithMany(v => v.Parkings)
				.HasForeignKey(p => p.VehicleId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ParkingModel>()
				.HasOne(p => p.Fare)
				.WithMany(f => f.Parkings)
				.HasForeignKey(p => p.FareId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<VehicleModel>()
				.HasMany(v => v.Parkings)
				.WithOne(p => p.Vehicle)
				.HasForeignKey(p => p.VehicleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<FareModel>()
				.Property(f => f.PricePerHour)
				.HasColumnType("decimal(18, 2)");

			builder.Entity<ParkingModel>()
				.Property(p => p.TotalPrice)
				.HasColumnType("decimal(18, 2)");

			base.OnModelCreating(builder);
		}
	}
}
