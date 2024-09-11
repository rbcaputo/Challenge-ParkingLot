using ParkingLotAPI.Dtos.Lot.Get;
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
					.ToList()
			};
		}
	}
}
