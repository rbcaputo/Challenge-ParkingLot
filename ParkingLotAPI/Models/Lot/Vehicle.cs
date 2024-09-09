using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Models.Lot
{
	public class Vehicle
	{
		[Key]
		public int Id { get; set; }
		
		public string LicensePlate { get; set; } = string.Empty;
		
		public VehicleSizes Size { get; set; }

		[NotMapped]
		public double SizeFareMod => VehicleSizeFareMods[Size];

		public string Brand { get; set; } = string.Empty;
		
		public string Model { get; set; } = string.Empty;
		
		public string Color { get; set; } = string.Empty;

		public ICollection<Parking> Parkings { get; set; } = [];
	}
}
