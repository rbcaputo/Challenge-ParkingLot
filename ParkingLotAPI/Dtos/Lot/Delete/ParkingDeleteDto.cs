namespace ParkingLotAPI.Dtos.Lot.Delete
{
	public class ParkingDeleteDto
	{
		public string LicensePlate { get; set; } = string.Empty;

		public DateTime EntryTime { get; set; }
	}
}
