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
		private string _licensePlate = string.Empty;

		[Key]
		public int Id { get; private set; }

		public string LicensePlate
		{
			get => _licensePlate;
			set
			{
				_licensePlate = value;
				ValidatorClass.ValidateLicensePlate(this);
			}
		}

		public VehicleSize Size { get; set; }

		[NotMapped]
		public decimal SizeFareMod => VehicleSizeFareMods[Size];

		public string Brand { get; set; } = string.Empty;

		public string Model { get; set; } = string.Empty;

		public string Color { get; set; } = string.Empty;

		public ICollection<ParkingModel> Parkings { get; set; } = [];

		public bool IsParked { get; set; }

		public void SetIsParked()
		{
			IsParked = ValidatorClass.CheckIfVechileIsParked(this);
		}
	}
}
