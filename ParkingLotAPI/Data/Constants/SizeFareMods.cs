namespace ParkingLotAPI.Data.Constants
{
	public static class SizeFareMods
	{
		public enum VehicleSizes
		{
			Small,
			Medium,
			Large
		}

		public static IReadOnlyDictionary<VehicleSizes, double> VehicleSizeFareMods { get; } = new Dictionary<VehicleSizes, double>()
		{
			{ VehicleSizes.Small, 1.00 },
			{ VehicleSizes.Medium, 1.50 },
			{ VehicleSizes.Large, 2.00 }
		};
	}
}
