using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Min;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Mappers.Lot
{
	public static class ParkingMapper
	{
		public static ParkingGetDto MapParkingModelToGetDto(ParkingModel parking)
		{
			return new ParkingGetDto
			{
				Fare = FareMapper.MapFareModelToGetDto(parking.Fare),

				Vehicle = VehicleMapper.MapVehicleModelToGetDto(parking.Vehicle),

				EntryTime = parking.EntryTime,

				ExitTime = parking.ExitTime,

				Duration = parking.Duration,

				TotalPrice = parking.TotalPrice
			};
		}

		public static ParkingMinGetDto MapParkingModelToGetMinDto(ParkingModel parking)
		{
			return new ParkingMinGetDto
			{
				Fare = FareMapper.MapFareModelToGetDto(parking.Fare),

				EntryTime = parking.EntryTime,

				ExitTime = parking.ExitTime,

				Duration = parking.Duration,

				TotalPrice = parking.TotalPrice
			};
		}
	}
}
