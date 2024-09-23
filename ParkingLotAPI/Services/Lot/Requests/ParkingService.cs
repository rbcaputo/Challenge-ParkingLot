using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut.Parking;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;

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
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0
					? []
					: parkings;
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
					.Where(p => p.Fare.PricePerHour == pricePerHour)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0
					? []
					: parkings;
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
					.Where(p => p.Duration.HasValue &&
											p.Duration.Value.TotalHours == duration.TotalHours)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0
					? []
					: parkings;
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
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.Select(p => ParkingMapper.MapParkingModelToGetDto(p))
					.ToListAsync(cancellation);

				return parkings.Count == 0
					? []
					: parkings;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool> AddParkingAsync(ParkingPostDto parkingDto, CancellationToken cancellation)
		{
			try
			{
				ParkingModel parking = await ParkingMapper.MapParkingPostDtoToModel(parkingDto, _context, cancellation);

				await _context.AddAsync(parking, cancellation);
				parking.Vehicle.IsParked = ValidatorClass.CheckIfVechileIsParked(parking.Vehicle);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> UpdateCurrentParkingByLicensePlateAsync(ParkingPutDto parkingDto, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? parking = await _context.Parkings
					.Where(p => p.Vehicle.LicensePlate == parkingDto.LicensePlate.Replace("-", "").ToUpper() &&
											p.ExitTime == null)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.FirstOrDefaultAsync(cancellation);

				if (parking == null)
					return null;

				ParkingMapper.MapParkingPutDtoToModel(parkingDto, parking);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> RemoveParkingByLicensePlateEntryTimeAsync(string licensePlate, DateTime entryTime, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? parking = await _context.Parkings
					.Where(p => p.Vehicle.LicensePlate == licensePlate.Replace("-", "").ToUpper() &&
											p.EntryTime == entryTime)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.FirstOrDefaultAsync(cancellation);


				if (parking == null)
					return null;

				VehicleModel vehicle = parking.Vehicle;

				_context.Parkings.Remove(parking);

				if (vehicle.Parkings.Count != 0)
					ValidatorClass.CheckIfVechileIsParked(vehicle);

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
