using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
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

				Vehicle = VehicleMapper.MapVehicleModelToGetDto(parking.Vehicle!),

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

		public static async Task<ParkingModel> MapParkingPostDtoToModel(ParkingPostPutDto parkingDto, DataContext context, CancellationToken cancellation)
		{
			try
			{
				VehicleModel? vehicle = await context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate.Equals(parkingDto.LicensePlate.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase), cancellation);

				if (vehicle == null)
					throw new InvalidOperationException($"{nameof(MapParkingPostDtoToModel)}: {nameof(vehicle)} cannot be null.");

				return new ParkingModel
				{
					Vehicle = vehicle,

					EntryTime = parkingDto.EntryTime,

					ExitTime = null
				};
			}
			catch
			{
				throw;
			}
		}

		public static void MapParkingPutDtoToModel(ParkingPostPutDto parkingDto, ParkingModel parking)
		{
			parking.ExitTime = parkingDto.ExitTime;
		}
	}
}
