using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;
using ParkingLotAPI.Utils;
using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Services.Lot.Requests
{
	public class VehicleService(DataContext context) : IVehicleService
	{
		private readonly DataContext _context = context;

		public async Task<ICollection<VehicleGetDto>> GetAllVehiclesAsync(CancellationToken cancellation)
		{
			try
			{
				ICollection<VehicleGetDto> vehicles = await _context.Vehicles
					.Include(v => v.Parkings)
					.Select(v => VehicleMapper.MapVehicleModelToGetDto(v))
					.ToListAsync(cancellation);

				return vehicles.Count == 0 ?
					[] :
					vehicles;
			}
			catch
			{
				throw;
			}

		}

		public async Task<ICollection<VehicleGetDto>> GetAllParkedVehiclesAsync(CancellationToken cancellation)
		{
			try
			{
				ICollection<VehicleGetDto> vehicles = await _context.Vehicles
					.Where(v => v.IsParked)
					.Select(v => VehicleMapper.MapVehicleModelToGetDto(v))
					.ToListAsync(cancellation);

				return vehicles.Count == 0 ?
					[] :
					vehicles;
			}
			catch
			{
				throw;
			}
		}

		public async Task<ICollection<VehicleGetDto>> GetAllVehiclesBySizeAsync(VehicleSize size, CancellationToken cancellation)
		{
			try
			{
				ICollection<VehicleGetDto> vehicles = await _context.Vehicles
					.Where(v => v.Size == size)
					.Include(v => v.Parkings)
					.Select(v => VehicleMapper.MapVehicleModelToGetDto(v))
					.ToListAsync(cancellation);

				return vehicles.Count == 0 ?
					[] :
					vehicles;
			}
			catch
			{
				throw;
			}
		}

		public async Task<VehicleGetDto?> GetVehicleByLicensePlateAsync(string licensePlate, CancellationToken cancellation)
		{
			try
			{
				VehicleGetDto? vehicle = await _context.Vehicles
					.Where(v => v.LicensePlate == licensePlate.Replace("-", "").ToUpper())
					.Include(v => v.Parkings)
					.Select(v => VehicleMapper.MapVehicleModelToGetDto(v))
					.FirstOrDefaultAsync(cancellation);

				return vehicle ?? null;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool> AddVehicleAsync(VehiclePostPutDto vehicleDto, CancellationToken cancellation)
		{
			try
			{
				VehicleModel? vehicle = await _context.Vehicles
					.Where(v => v.LicensePlate == vehicleDto.LicensePlate.Replace("-", "").ToUpper())
					.FirstOrDefaultAsync(cancellation);

				if (vehicle != null)
					throw new InvalidOperationException($"{nameof(VehicleService)}: {nameof(AddVehicleAsync)}: {nameof(vehicle)} with same license plate already exists.");

				VehicleModel newVehicle = VehicleMapper.MapVehiclePostDtoToModel(vehicleDto);

				await _context.Vehicles.AddAsync(newVehicle, cancellation);
				newVehicle.IsParked = ValidatorClass.CheckIfVechileIsParked(newVehicle);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> UpdateVehicleByLicensePlateAsync(VehiclePostPutDto vehicleDto, CancellationToken cancellation)
		{
			try
			{
				VehicleModel? vehicle = await _context.Vehicles
					.Where(v => v.LicensePlate == vehicleDto.LicensePlate.Replace("-", "").ToUpper())
					.FirstOrDefaultAsync(cancellation);

				if (vehicle == null)
					return null;

				VehicleMapper.MapVehiclePutDtoToModel(vehicleDto, vehicle);
				await _context.SaveChangesAsync(cancellation);

				return true;
			}
			catch
			{
				throw;
			}
		}

		public async Task<bool?> RemoveVehicleByLicensePlateAsync(string licensePlate, CancellationToken cancellation)
		{
			try
			{
				VehicleModel? vehicle = await _context.Vehicles
					.Where(v => v.LicensePlate == licensePlate.Replace("-", "").ToUpper())
					.FirstOrDefaultAsync(cancellation);

				if (vehicle == null)
					return null;

				_context.Vehicles.Remove(vehicle);
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
