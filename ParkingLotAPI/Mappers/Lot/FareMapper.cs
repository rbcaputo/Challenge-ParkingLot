using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut.Fare;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;

namespace ParkingLotAPI.Mappers.Lot
{
	public static class FareMapper
	{
		public static FareGetDto MapFareModelToGetDto(FareModel fare)
		{
			return new()
			{
				StartDate = fare.StartDate.Date,

				EndDate = fare.EndDate,

				PricePerHour = fare.PricePerHour,

				IsCurrent = fare.IsCurrent
			};
		}

		public static FareModel MapFarePostDtoToModel(FarePostDto fareDto)
		{
			return new()
			{
				StartDate = fareDto.StartDate.Date,

				EndDate = fareDto.EndDate,

				PricePerHour = fareDto.PricePerHour,
			};
		}

		public static void MapFarePutDtoToModel(FarePutDto fareDto, FareModel fare)
		{
			fare.EndDate = fareDto.EndDate;

			fare.PricePerHour = fareDto.PricePerHour;

			fare.IsCurrent = ValidatorClass.CheckIfFareIsCurrent(fare);
		}
	}
}
