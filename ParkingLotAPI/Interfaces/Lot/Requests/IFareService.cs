﻿using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;

namespace ParkingLotAPI.Interfaces.Lot.Requests
{
	public interface IFareService
	{
		public Task<ICollection<FareGetDto>> GetAllFaresAsync(CancellationToken cancellation);
		public Task<ICollection<FareGetDto>> GetAllFaresByPricePerHourAsync(decimal pricePerHour, CancellationToken cancellation);
		public Task<FareGetDto?> GetCurrentFareAsync(CancellationToken cancellation);

		public Task<bool> AddFareAsync(FarePostPutDto fareDto, CancellationToken cancellation);
		public Task<bool?> UpdateCurrentFareAsync(FarePostPutDto fareDto, CancellationToken cancellation);
		public Task<bool?> RemoveFareByStartDateAsync(DateTime startDate, CancellationToken cancellation);
	}
}
