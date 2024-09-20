namespace ParkingLotAPI.Dtos.Lot.PostPut.Fare
{
	public class FarePostDto
	{
		public DateTime StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public decimal PricePerHour { get; set; }
	}
}
