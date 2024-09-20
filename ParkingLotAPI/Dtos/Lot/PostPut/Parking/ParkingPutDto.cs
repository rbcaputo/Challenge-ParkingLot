namespace ParkingLotAPI.Dtos.Lot.PostPut.Parking
{
	public class ParkingPutDto
	{
		public string LicensePlate { get; set; } = string.Empty;

		public DateTime ExitTime { get; set; }
	}
}
