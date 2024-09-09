namespace ParkingLotAPI.Utils
{
	public static class FareValidator
	{
		public static void ValidateDuration(DateTime startDate, DateTime? endDate)
		{
			if (endDate.HasValue &&
					endDate < startDate)
				throw new ArgumentException("EndDate cannot be before StartDate.");
		}

		public static void ValidatePricePerHour(decimal pricePerHour)
		{
			if (pricePerHour <= 0)
				throw new ArgumentException("PricePerHour cannot be zero or negative.");
		}

		public static void ValidateExitTime(DateTime entryTime, DateTime? exitTime)
		{
			if (exitTime.HasValue &&
					exitTime < entryTime)
				throw new ArgumentException("ExitTime cannot be before EntryTime.");
		}
	}
}
