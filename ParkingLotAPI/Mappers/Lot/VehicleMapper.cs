using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Models.Lot;

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
				LicensePlate = vehicleDto.LicensePlate,

				Size = vehicleDto.Size,

				Brand = vehicleDto.Brand,

				Model = vehicleDto.Model,

				Color = vehicleDto.Color
			};
		}

		public static void MapVehiclePutDtoToModel(VehiclePostPutDto vehicleDto, VehicleModel vehicle)
		{
			vehicle.LicensePlate = vehicleDto.LicensePlate;

			vehicle.Size = vehicleDto.Size;

			vehicle.Brand = vehicleDto.Brand;

			vehicle.Model = vehicleDto.Model;

			vehicle.Color = vehicleDto.Color;
		}

		public static bool CompareVehicleModelToPutDto(VehicleModel vehicle, VehiclePostPutDto vehicleDto)
		{
			if (vehicle == null || vehicleDto == null)
				return false;

			return vehicle.LicensePlate.Equals(vehicleDto.LicensePlate, StringComparison.InvariantCultureIgnoreCase) &&
						 vehicle.Size == vehicleDto.Size &&
						 vehicle.Brand.Equals(vehicleDto.Brand, StringComparison.InvariantCultureIgnoreCase) &&
						 vehicle.Model.Equals(vehicleDto.Model, StringComparison.InvariantCultureIgnoreCase) &&
						 vehicle.Color.Equals(vehicleDto.Color, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
