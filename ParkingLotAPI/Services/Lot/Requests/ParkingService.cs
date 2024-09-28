using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Delete;
using ParkingLotAPI.Dtos.Lot.Get;
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

		public async Task<bool> AddParkingAsync(string licensePlate, CancellationToken cancellation)
		{
			try
			{
				ParkingModel parking = await ParkingMapper.MapParkingPostToModel(licensePlate, _context, cancellation);

				await _context.AddAsync(parking, cancellation);
				parking.Vehicle.IsParked = ValidatorClass.CheckIfVehicleIsParked(parking.Vehicle);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> UpdateCurrentParkingByLicensePlateAsync(string licensePlate, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? parking = await _context.Parkings
					.Where(p => p.Vehicle.LicensePlate == licensePlate.Replace("-", "").ToUpper() &&
											p.ExitTime == null)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.FirstOrDefaultAsync(cancellation);

				if (parking == null)
					return null;

				ParkingMapper.MapParkingPutToModel(parking);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> RemoveParkingByLicensePlateEntryTimeAsync(ParkingDeleteDto parkingDto, CancellationToken cancellation)
		{
			try
			{
				ParkingModel? parking = await _context.Parkings
					.Where(p => p.Vehicle.LicensePlate == parkingDto.LicensePlate.Replace("-", "").ToUpper() &&
											p.EntryTime == parkingDto.EntryTime)
					.Include(p => p.Fare)
					.Include(p => p.Vehicle)
					.FirstOrDefaultAsync(cancellation);


				if (parking == null)
					return null;

				VehicleModel vehicle = parking.Vehicle;

				_context.Parkings.Remove(parking);

				if (vehicle.Parkings.Count != 0)
					ValidatorClass.CheckIfVehicleIsParked(vehicle);

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
