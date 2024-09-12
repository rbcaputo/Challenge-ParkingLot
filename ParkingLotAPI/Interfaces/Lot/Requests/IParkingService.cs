using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;

namespace ParkingLotAPI.Interfaces.Lot.Requests
{
	public interface IParkingService
	{
		public Task<ICollection<ParkingGetDto>> GetAllParkingsAsync(CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByPricePerHourAsync(decimal pricePerHour, CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByDurationAsync(TimeSpan duration, CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByCurrentFareAsync(CancellationToken cancellation);

		public Task<bool> AddFareAsync(ParkingPostPutDto parkingDto, CancellationToken cancellation);
		public Task<bool?> UpdateParkingByLicensePlate(string licensePlate, ParkingPostPutDto parkingDto, CancellationToken cancellation);
		public Task<bool?> RemoveParkingByLicensePlate(string licensePlate, CancellationToken cancellation);
	}
}
