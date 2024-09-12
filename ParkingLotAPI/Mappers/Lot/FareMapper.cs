using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
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

		public static FareModel MapFarePostDtoToModel(FarePostPutDto fareDto)
		{
			return new FareModel
			{
				StartDate = fareDto.StartDate,

				EndDate = fareDto.EndDate,

				PricePerHour = fareDto.PricePerHour
			};
		}

		public static void MapFarePutDtoToModel(FarePostPutDto fareDto, FareModel fare)
		{
			fare.StartDate = fareDto.StartDate;

			fare.EndDate = fareDto.EndDate;

			fare.PricePerHour = fareDto.PricePerHour;
		}
	}
}
