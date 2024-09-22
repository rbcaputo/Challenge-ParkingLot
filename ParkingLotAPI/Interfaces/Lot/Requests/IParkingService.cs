using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut.Parking;

namespace ParkingLotAPI.Interfaces.Lot.Requests
{
	public interface IParkingService
	{
		public Task<ICollection<ParkingGetDto>> GetAllParkingsAsync(CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByPricePerHourAsync(decimal pricePerHour, CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByDurationAsync(TimeSpan duration, CancellationToken cancellation);
		public Task<ICollection<ParkingGetDto>> GetAllParkingsByCurrentFareAsync(CancellationToken cancellation);

		public Task<bool> AddParkingAsync(ParkingPostDto parkingDto, CancellationToken cancellation);
		public Task<bool?> UpdateCurrentParkingByLicensePlateAsync(ParkingPutDto parkingDto, CancellationToken cancellation);
		public Task<bool?> RemoveParkingByLicensePlateEntryTimeAsync(string licensePlate, DateTime entryTime, CancellationToken cancellation);
	}
}
