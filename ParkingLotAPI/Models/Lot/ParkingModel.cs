using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class ParkingModel
	{
		private DateTime? _exitTime;

		[Key]
		public int Id { get; private set; }

		public required FareModel Fare { get; set; }

		[ForeignKey(nameof(Fare))]
		public int FareId { get; set; }

		public required VehicleModel Vehicle { get; set; }

		[ForeignKey(nameof(Vehicle))]
		public int VehicleId { get; set; }

		public DateTime EntryTime { get; set; } = DateTime.Now;

		public DateTime? ExitTime
		{
			get => _exitTime;
			set
			{
				_exitTime = value;
				ValidatorClass.ValidateExitTime(this);
			}
		}

		public TimeSpan? Duration { get; set; }

		public decimal? TotalPrice { get; set; }
	}
}
