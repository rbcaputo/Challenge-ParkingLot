namespace ParkingLotAPI.Data.Constants
{
	public static class SizeFareMods
	{
		public enum VehicleSize
		{
			small = 1,
			medium = 2,
			large = 3
		}

		public static IReadOnlyDictionary<VehicleSize, decimal> VehicleSizeFareMods { get; } = new Dictionary<VehicleSize, decimal>()
		{
			{ VehicleSize.small, 1.00m },
			{ VehicleSize.medium, 1.25m },
			{ VehicleSize.large, 1.50m }
		};
	}
}
