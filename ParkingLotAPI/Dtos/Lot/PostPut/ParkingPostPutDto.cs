namespace ParkingLotAPI.Dtos.Lot.PostPut
{
	public class ParkingPostPutDto
	{
		public string LicensePlate { get; set; } = string.Empty;

		public DateTime EntryTime { get; set; } = DateTime.UtcNow;

		public DateTime? ExitTime { get; set; }
	}
}
