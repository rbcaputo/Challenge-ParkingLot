using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Utils
{
	public static class ValidatorClass
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

		public static bool CheckIfFareIsCurrent(FareModel fare)
		{
			return (fare.StartDate <= DateTime.Now) &&
						 (!fare.EndDate.HasValue || fare.EndDate >= DateTime.Now);
		}

		public static bool CheckIfVechileIsParked(VehicleModel vehicle)
		{
			return vehicle.Parkings.
				Any(p => p.ExitTime == null);
		}
	}
}
