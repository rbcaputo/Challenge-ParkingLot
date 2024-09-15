using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;

namespace ParkingLotAPI.Mappers.Lot
{
	public static class VehicleMapper
	{
		public static VehicleGetDto MapVehicleModelToGetDto(VehicleModel vehicle)
		{
			return new VehicleGetDto
			{
				LicensePlate = vehicle.LicensePlate,

				Size = vehicle.Size,

				Brand = vehicle.Brand,

				Model = vehicle.Model,

				Color = vehicle.Color,

				Parkings = vehicle.Parkings
					.Select(ParkingMapper.MapParkingModelToGetMinDto)
					.ToList(),

				IsParked = vehicle.IsParked
			};
		}

		public static VehicleModel MapVehiclePostDtoToModel(VehiclePostPutDto vehicleDto)
		{
			return new VehicleModel
			{
				LicensePlate = StringProcessorClass.ToTitleCase(vehicleDto.LicensePlate.Replace("-", "").ToUpperInvariant()),

				Size = vehicleDto.Size,

				Brand = StringProcessorClass.ToTitleCase(vehicleDto.Brand),

				Model = StringProcessorClass.ToTitleCase(vehicleDto.Model),

				Color = StringProcessorClass.ToTitleCase(vehicleDto.Color)
			};
		}

		public static void MapVehiclePutDtoToModel(VehiclePostPutDto vehicleDto, VehicleModel vehicle)
		{
			vehicle.LicensePlate = StringProcessorClass.ToTitleCase(vehicleDto.LicensePlate.Replace("-", "").ToUpperInvariant());

			vehicle.Size = vehicleDto.Size;

			vehicle.Brand = StringProcessorClass.ToTitleCase(vehicleDto.Brand);

			vehicle.Model = StringProcessorClass.ToTitleCase(vehicleDto.Model);

			vehicle.Color = StringProcessorClass.ToTitleCase(vehicleDto.Color);
		}
	}
}
