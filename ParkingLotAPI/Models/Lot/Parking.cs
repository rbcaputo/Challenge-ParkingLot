namespace ParkingLotAPI.Models.Lot
{
	public class Parking
	{
		public int Id { get; set; }
		public List<Vehicle> Vehicles { get; set; } = [];
		public int PricingId { get; set; }
		public Pricing Pricing { get; set; } = new();
		public DateTime EnterDate { get; set; }
		public DateTime? ExitDate { get; set; }
		public TimeSpan Duration { get; set; }
		public double TotalPrice { get; set; }
	}
}
