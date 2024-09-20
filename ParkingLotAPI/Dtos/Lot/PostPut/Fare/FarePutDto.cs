namespace ParkingLotAPI.Dtos.Lot.PostPut.Fare
{
	public class FarePutDto
	{
		public DateTime? EndDate { get; set; }

		public decimal PricePerHour { get; set; } = 5.00m;
	}
}
