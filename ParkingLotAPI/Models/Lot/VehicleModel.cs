using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Models.Lot
{
	[Index(nameof(LicensePlate), IsUnique = true)]
	public class VehicleModel
	{
		[Key]
		public int Id { get; set; }

		public string LicensePlate { get; set; } = string.Empty;

		public VehicleSize Size { get; set; }

		[NotMapped]
		public double SizeFareMod => VehicleSizeFareMods[Size];

		public string Brand { get; set; } = string.Empty;

		public string Model { get; set; } = string.Empty;

		public string Color { get; set; } = string.Empty;

		public ICollection<ParkingModel> Parkings { get; set; } = [];

		public bool IsParked => ValidatorClass.CheckIfVechileIsParked(this);
	}
}
