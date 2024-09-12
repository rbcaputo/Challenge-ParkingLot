namespace ParkingLotAPI.Dtos.Lot.PostPut
{
	public class FarePostPutDto
	{
		public DateTime StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public decimal PricePerHour { get; set; }
	}
}
