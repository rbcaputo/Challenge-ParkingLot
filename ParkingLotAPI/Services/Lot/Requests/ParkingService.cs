using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Services.Lot.Requests
{
	public class ParkingService(DataContext context) : IParkingService
	{
		private readonly DataContext _context = context;

		public async Task<ICollection<ParkingGetDto>> GetAllParkingsAsync(CancellationToken cancellation)
		{
			try
			{
				ICollection<ParkingGetDto> parkings = await _context.Parkings
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0 ?
					[] :
					parkings;
			}
			catch
			{
				throw;
			}
		}

		public async Task<ICollection<ParkingGetDto>> GetAllParkingsByPricePerHourAsync(decimal pricePerHour, CancellationToken cancellation)
		{
			try
			{
				ICollection<ParkingGetDto> parkings = await _context.Parkings
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0 ?
					[] :
					parkings;
			}
			catch
			{
				throw;
			}
		}

		public async Task<ICollection<ParkingGetDto>> GetAllParkingsByDurationAsync(TimeSpan duration, CancellationToken cancellation)
		{
			try
			{
				ICollection<ParkingGetDto> parkings = await _context.Parkings
					.Where(p => p.Duration == duration)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0 ?
					[] :
					parkings;
			}
			catch
			{
				throw;
			}
		}

		public async Task<ICollection<ParkingGetDto>> GetAllParkingsByCurrentFareAsync(CancellationToken cancellation)
		{
			try
			{
				ICollection<ParkingGetDto> parkings = await _context.Parkings
					.Where(p => p.Fare.IsCurrent)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0 ?
					[] :
					parkings;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool> AddParkingAsync(ParkingPostPutDto parkingDto, CancellationToken cancellation)
		{
			try
			{
				await _context.AddAsync(ParkingMapper.MapParkingPostDtoToModel(parkingDto), cancellation);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> UpdateCurrentParkingByLicensePlateAsync(ParkingPostPutDto parkingDto, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? currentParking = await _context.Parkings
					.Where(p => p.Vehicle.LicensePlate.Equals(parkingDto.Vehicle.LicensePlate, StringComparison.InvariantCultureIgnoreCase) &&
										 p.Vehicle.IsParked)
					.FirstOrDefaultAsync(cancellation);

				if (currentParking == null)
					return null;

				ParkingMapper.MapParkingPutDtoToModel(parkingDto, currentParking);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> RemoveParkingByLicensePlateAsync(string licensePlate, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? parking = await _context.Parkings
					.FirstOrDefaultAsync(p => p.Vehicle.LicensePlate.Equals(licensePlate, StringComparison.InvariantCultureIgnoreCase), cancellation);

				if (parking == null)
					return null;

				_context.Parkings.Remove(parking);
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
