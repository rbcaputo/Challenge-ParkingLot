using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;

namespace ParkingLotAPI.Interfaces.Lot.Requests
{
	public interface IVehicleService
	{
		public Task<ICollection<VehicleGetDto>> GetAllVehiclesAsync(CancellationToken cancellation);
		public Task<ICollection<VehicleGetDto>> GetAllParkedVehiclesAsync(CancellationToken cancellation);
		public Task<VehicleGetDto?> GetVehicleByLicensePlateAsync(string licensePlate, CancellationToken cancellation);

		public Task<bool> AddVehicleAsync(VehiclePostPutDto vehicleDto, CancellationToken cancellation);
		public Task<bool?> UpdateVehicleByLicensePlateAsync(VehiclePostPutDto vehicleDto, CancellationToken cancellation);
		public Task<bool?> RemoveVehicleByLicensePlateAsync(string licensePlate, CancellationToken cancellation);

	}
}
