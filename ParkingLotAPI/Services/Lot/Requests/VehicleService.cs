using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;
using ParkingLotAPI.Models.Lot;
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
					.Where(v => v.IsParked == true)
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
					.Where(v => v.LicensePlate.Equals(licensePlate.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase))
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
				await _context.Vehicles.AddAsync(VehicleMapper.MapVehiclePostDtoToModel(vehicleDto), cancellation);
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
				VehicleModel? vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate.Equals(vehicleDto.LicensePlate.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase), cancellation);

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
				VehicleModel? vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate.Equals(licensePlate.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase), cancellation);

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
