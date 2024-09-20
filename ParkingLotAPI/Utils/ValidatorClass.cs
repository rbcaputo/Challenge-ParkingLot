using ParkingLotAPI.Models.Lot;
using System.Text.RegularExpressions;

namespace ParkingLotAPI.Utils
{
	public static class ValidatorClass
	{
		public static void ValidateLicensePlate(VehicleModel vehicle)
		{
			string stringFormat = @"^[a-zA-Z]{3}(?:-\d{4}|\d{4})$";

			if (!Regex.IsMatch(vehicle.LicensePlate, stringFormat))
				throw new InvalidOperationException($"{nameof(ValidatorClass)}: {nameof(ValidateLicensePlate)}: {nameof(vehicle.LicensePlate)} only valid formats are ABC-1234 and ABC1234.");
		}

		public static void ValidateFareDuration(FareModel fare)
		{
			if (fare.StartDate < DateTime.Now.Date)
				throw new InvalidOperationException($"{nameof(ValidatorClass)}: {nameof(ValidateFareDuration)}: {nameof(fare.StartDate)} cannot be in the past.");

			if (fare.EndDate.HasValue &&
					fare.EndDate < fare.StartDate)
				throw new InvalidOperationException($"{nameof(ValidatorClass)}: {nameof(ValidateFareDuration)}: {nameof(fare.EndDate)} cannot be before {nameof(fare.StartDate)}.");
		}

		public static void ValidatePricePerHour(FareModel fare)
		{
			if (fare.PricePerHour <= 0)
				throw new InvalidOperationException($"{nameof(ValidatorClass)}: {nameof(ValidatePricePerHour)}: {nameof(fare.PricePerHour)} cannot be zero or negative.");
		}

		public static void ValidateExitTime(ParkingModel parking)
		{
			if (parking.ExitTime.HasValue &&
					parking.ExitTime < parking.EntryTime)
				throw new InvalidOperationException($"{nameof(ValidatorClass)}: {nameof(ValidateExitTime)}: {nameof(parking.ExitTime)} cannot be before {nameof(parking.EntryTime)}.");
		}

		public static bool CheckIfFareIsCurrent(FareModel fare)
		{
			return fare.EndDate == null ||
						(fare.EndDate.HasValue && fare.EndDate.Value.Date > DateTime.Now.Date);
		}

		public static bool CheckIfVechileIsParked(VehicleModel vehicle)
		{
			return vehicle.Parkings.Count != 0 &&
						 vehicle.Parkings.Any(p => p.ExitTime == null);
		}
	}
}
