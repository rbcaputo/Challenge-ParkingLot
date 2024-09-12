namespace ParkingLotAPI.Data.Constants
{
	public static class SizeFareMods
	{
		public enum VehicleSize
		{
			Small,
			Medium,
			Large
		}

		public static IReadOnlyDictionary<VehicleSize, double> VehicleSizeFareMods { get; } = new Dictionary<VehicleSize, double>()
		{
			{ VehicleSize.Small, 1.00 },
			{ VehicleSize.Medium, 1.50 },
			{ VehicleSize.Large, 2.00 }
		};
	}
}
