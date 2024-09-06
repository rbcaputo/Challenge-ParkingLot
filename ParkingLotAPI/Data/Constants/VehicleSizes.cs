namespace ParkingLotAPI.Data.Constants
{
	public static class VehicleSizes
	{
		public static IReadOnlyDictionary<string, double> SizePrices { get; } = new Dictionary<string, double>()
		{
			{ "s", 1.00 },
			{ "m", 1.50 },
			{ "l", 2.00 }
		};
	}
}
