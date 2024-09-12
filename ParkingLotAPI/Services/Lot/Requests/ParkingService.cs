using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Data;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Mappers.Lot;

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

		//
	}
}
