using ParkingLotAPI.Models.Lot;

namespace ParkingLotAPI.Dtos.Lot.PostPut
{
	public class ParkingPostPutDto
	{
		public VehicleModel Vehicle { get; set; } = new();

		public DateTime EntryTime { get; set; } = DateTime.UtcNow;

		public DateTime? ExitTime { get; set; }
	}
}
