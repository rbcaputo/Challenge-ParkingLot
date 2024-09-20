using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Dtos.Min
{
	public class VehicleMinGetDto
	{
		public string LicensePlate { get; set; } = string.Empty;

		public VehicleSize Size { get; set; }

		public string Brand { get; set; } = string.Empty;

		public string Model { get; set; } = string.Empty;

		public string Color { get; set; } = string.Empty;
	}
}
