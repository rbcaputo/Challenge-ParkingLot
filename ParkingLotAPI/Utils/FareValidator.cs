using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Utils
{
	public static class FareValidator
	{
		public static void ValidateDuration(DateTime startDate, DateTime? endDate)
		{
			if (endDate.HasValue &&
					endDate < startDate)
				throw new ArgumentException($"{nameof(endDate)} cannot be before {nameof(startDate)}.");
		}

		public static void ValidatePricePerHour(decimal pricePerHour)
		{
			if (pricePerHour <= 0)
				throw new ArgumentException($"{nameof(pricePerHour)} cannot be zero or negative.");
		}

		public static void ValidateExitTime(DateTime entryTime, DateTime? exitTime)
		{
			if (exitTime.HasValue &&
					exitTime < entryTime)
				throw new ArgumentException($"{nameof(exitTime)} cannot be before {nameof(entryTime)}.");
		}

		public static decimal CalculateTotalPrice(ParkingModel parking)
		{
			if (parking.Fare == null)
				throw new ArgumentException($"{nameof(parking.Fare)} cannot be null.");

			if (parking.Vehicle == null)
				throw new ArgumentException($"{nameof(parking.Vehicle)} cannot be null.");

			decimal adjustedPricePerHour = parking.Fare.PricePerHour * (decimal)parking.Vehicle.SizeFareMod;
			double totalMinutes = parking.Duration.TotalMinutes;

			if (totalMinutes <= 30)
				return adjustedPricePerHour / 2;

			int fullHours = (int)(totalMinutes / 60);
			double remainingMinutes = totalMinutes % 60;

			if (remainingMinutes > 10)
				fullHours++;

			return fullHours * adjustedPricePerHour;
		}
	}
}
