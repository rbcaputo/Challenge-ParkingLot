using ParkingLotAPI.Dtos.Min;
using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Dtos.Lot.Get
{
	public class VehicleGetDto
	{
		public string LicensePlate { get; set; } = string.Empty;

		public VehicleSize Size { get; set; }

		public string Brand { get; set; } = string.Empty;

		public string Model { get; set; } = string.Empty;

		public string Color { get; set; } = string.Empty;

		public ICollection<ParkingMinGetDto> Parkings { get; set; } = [];

		public bool IsParked { get; set; }
	}
}
