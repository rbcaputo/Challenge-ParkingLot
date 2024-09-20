namespace ParkingLotAPI.Dtos.Min
{
	public class ParkingMinGetDto
	{
		public DateTime EntryTime { get; set; }

		public DateTime? ExitTime { get; set; }

		public TimeSpan? Duration { get; set; }

		public decimal? TotalPrice { get; set; }
	}
}
