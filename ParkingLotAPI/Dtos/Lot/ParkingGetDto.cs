namespace ParkingLotAPI.Dtos.Lot
{
	public class ParkingGetDto
	{
		public FareGetDto Fare { get; set; } = new();

		public VehicleGetDto Vehicle { get; set; } = new();

		public DateTime EntryTime { get; set; }

		public DateTime? ExitTime { get; set; }

		public TimeSpan Duration { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
