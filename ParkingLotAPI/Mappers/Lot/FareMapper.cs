using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Mappers.Lot
{
	public static class FareMapper
	{
		public static FareGetDto MapFareModelToGetDto(FareModel fare)
		{
			return new FareGetDto
			{
				StartDate = fare.StartDate,

				EndDate = fare.EndDate,

				PricePerHour = fare.PricePerHour,

				IsCurrent = fare.IsCurrent
			};
		}
	}
}
