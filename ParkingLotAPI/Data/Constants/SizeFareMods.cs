namespace ParkingLotAPI.Data.Constants
{
	public static class SizeFareMods
	{
		public enum VehicleSize
		{
			Small = 1,
			Medium = 2,
			Large = 3
		}

		public static IReadOnlyDictionary<VehicleSize, decimal> VehicleSizeFareMods { get; } = new Dictionary<VehicleSize, decimal>()
		{
			{ VehicleSize.Small, 1.00m },
			{ VehicleSize.Medium, 1.50m },
			{ VehicleSize.Large, 2.00m }
		};
	}
}
