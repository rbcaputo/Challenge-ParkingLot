using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Utils
{
	public static class CalculatorClass
	{
		public static TimeSpan? CalculateDuration(ParkingModel parking)
		{
			return parking.ExitTime == null ?
				throw new InvalidOperationException($"{nameof(CalculatorClass)}: {nameof(CalculateDuration)}: {nameof(parking.ExitTime)} cannot be null.") :
				parking.ExitTime - parking.EntryTime;
		}

		public static decimal CalculateTotalPrice(ParkingModel parking)
		{
			if (parking.Fare == null)
				throw new InvalidOperationException($"{nameof(CalculatorClass)}: {nameof(parking.Fare)} cannot be null.");

			if (parking.Vehicle == null)
				throw new InvalidOperationException($"{nameof(CalculatorClass)}: {nameof(parking.Vehicle)} cannot be null.");

			if (parking.Duration == null)
				throw new InvalidOperationException($"{nameof(CalculatorClass)}: {nameof(parking.Duration)} cannot be null.");

			decimal adjustedPricePerHour = parking.Vehicle.SizeFareMod * parking.Fare.PricePerHour;
			double totalMinutes = parking.Duration.Value.TotalMinutes;

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
