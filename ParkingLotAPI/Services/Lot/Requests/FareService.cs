using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;

namespace ParkingLotAPI.Services.Lot.Requests
{
	public class FareService(DataContext context) : IFareService
	{
		private readonly DataContext _context = context;

		public async Task<ICollection<FareGetDto>> GetAllFaresAsync(CancellationToken cancellation)
		{
			try
			{
				ICollection<FareGetDto> fares = await _context.Fares
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.ToListAsync(cancellation);

				return fares.Count == 0
					? []
					: fares;
			}
			catch
			{
				throw;
			}
		}

		public async Task<ICollection<FareGetDto>> GetAllFaresByPricePerHourAsync(decimal pricePerHour, CancellationToken cancellation)
		{
			try
			{
				ICollection<FareGetDto> fares = await _context.Fares
					.Where(f => f.PricePerHour == pricePerHour)
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.ToListAsync(cancellation);

				return fares.Count == 0
					? []
					: fares;
			}
			catch
			{
				throw;
			}
		}

		public async Task<FareGetDto?> GetFareByStartDateAsync(DateTime startDate, CancellationToken cancellation)
		{
			try
			{
				FareGetDto? fare = await _context.Fares
					.Where(f => f.StartDate.Date == startDate.Date)
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(cancellation);

				return fare ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<FareGetDto?> GetFareByEndDateAsync(DateTime endDate, CancellationToken cancellation)
		{
			try
			{
				FareGetDto? fare = await _context.Fares
					.Where(f => f.EndDate.HasValue &&
											f.EndDate.Value.Date == endDate.Date)
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(cancellation);

				return fare ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<FareGetDto?> GetCurrentFareAsync(CancellationToken cancellation)
		{
			try
			{
				FareGetDto? fare = await _context.Fares
					.Where(f => f.IsCurrent)
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(cancellation);

				return fare ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool> AddFareAsync(FarePostPutDto fareDto, CancellationToken cancellation)
		{
			try
			{
				FareModel fare = FareMapper.MapFarePostDtoToModel(fareDto);

				await _context.Fares.AddAsync(fare, cancellation);
				fare.IsCurrent = ValidatorClass.CheckIfFareIsCurrent(fare);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> UpdateCurrentFareAsync(FarePostPutDto fareDto, CancellationToken cancellation)
		{
			try
			{
				FareModel? fare = await _context.Fares
					.Where(f => f.IsCurrent)
					.FirstOrDefaultAsync(cancellation);

				if (fare == null)
					return null;

				FareMapper.MapFarePutDtoToModel(fareDto, fare);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> RemoveFareByStartDateAsync(DateTime startDate, CancellationToken cancellation)
		{
			try
			{
				FareModel? fare = await _context.Fares
					.Where(f => f.StartDate.Date == startDate.Date)
					.FirstOrDefaultAsync(cancellation);

				if (fare == null)
					return null;

				_context.Fares.Remove(fare);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}
	}
}
