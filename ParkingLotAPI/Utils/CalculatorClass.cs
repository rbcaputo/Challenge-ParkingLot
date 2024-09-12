using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Utils
{
	public static class CalculatorClass
	{
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
