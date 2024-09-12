using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;

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

				return fares.Count == 0 ?
					[] :
					fares;
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

				return fares.Count == 0 ?
					[] :
					fares;
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
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(f => f.StartDate == startDate, cancellation);

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
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(f => f.EndDate.HasValue &&
																		f.EndDate.Value == endDate, cancellation);

				return fare ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<FareGetDto?> GetCurrentFareDtoAsync(CancellationToken cancellation)
		{
			try
			{
				FareGetDto? fare = await _context.Fares
					.Select(f => FareMapper.MapFareModelToGetDto(f))
					.FirstOrDefaultAsync(f => f.IsCurrent, cancellation);

				return fare ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<FareModel?> GetCurrentFareModelAsync(CancellationToken cancellation)
		{
			try
			{
				FareModel? fare = await _context.Fares
					.FirstOrDefaultAsync(f => f.IsCurrent);

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
				await _context.Fares
					.AddAsync(FareMapper.MapFarePostDtoToModel(fareDto), cancellation);
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
					.FirstOrDefaultAsync(f => f.IsCurrent, cancellation);

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
					.FirstOrDefaultAsync(f => f.StartDate == startDate, cancellation);

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
