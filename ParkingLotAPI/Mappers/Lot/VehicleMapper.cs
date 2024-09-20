using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Dtos.Min;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;

namespace ParkingLotAPI.Mappers.Lot
{
	public static class VehicleMapper
	{
		public static VehicleGetDto MapVehicleModelToGetDto(VehicleModel vehicle)
		{
			return new()
			{
				LicensePlate = vehicle.LicensePlate,

				Size = vehicle.Size,

				Brand = vehicle.Brand,

				Model = vehicle.Model,

				Color = vehicle.Color,

				Parkings = vehicle.Parkings
					.Select(ParkingMapper.MapParkingModelToMinGetDto)
					.ToList(),

				IsParked = vehicle.IsParked
			};
		}

		public static VehicleMinGetDto MapVehicleModelToMinGetDto(VehicleModel vehicle)
		{
			return new()
			{
				LicensePlate = vehicle.LicensePlate,

				Size = vehicle.Size,

				Brand = vehicle.Brand,

				Model = vehicle.Model,

				Color = vehicle.Color
			};
		}

		public static VehicleModel MapVehiclePostDtoToModel(VehiclePostPutDto vehicleDto)
		{
			return new()
			{
				LicensePlate = FormaterClass.ToUpperCase(vehicleDto.LicensePlate.Replace("-", "")),

				Size = vehicleDto.Size,

				Brand = FormaterClass.ToTitleCase(vehicleDto.Brand),

				Model = FormaterClass.ToTitleCase(vehicleDto.Model),

				Color = FormaterClass.ToTitleCase(vehicleDto.Color)
			};
		}

		public static void MapVehiclePutDtoToModel(VehiclePostPutDto vehicleDto, VehicleModel vehicle)
		{
			vehicle.LicensePlate = FormaterClass.ToUpperCase(vehicleDto.LicensePlate.Replace("-", ""));

			vehicle.Size = vehicleDto.Size;

			vehicle.Brand = FormaterClass.ToTitleCase(vehicleDto.Brand);

			vehicle.Model = FormaterClass.ToTitleCase(vehicleDto.Model);

			vehicle.Color = FormaterClass.ToTitleCase(vehicleDto.Color);
		}
	}
}
