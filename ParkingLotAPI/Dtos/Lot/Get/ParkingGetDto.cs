using ParkingLotAPI.Dtos.Min;

namespace ParkingLotAPI.Dtos.Lot.Get
{
	public class ParkingGetDto
	{
		public FareGetDto? Fare { get; set; }

		public VehicleMinGetDto? Vehicle { get; set; }

		public DateTime EntryTime { get; set; }

		public DateTime? ExitTime { get; set; }

		public TimeSpan? Duration { get; set; }

		public decimal? TotalPrice { get; set; }
	}
}
