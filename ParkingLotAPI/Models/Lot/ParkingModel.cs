using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class ParkingModel
	{
		private DateTime? _exitTime;

		[Key]
		public int Id { get; set; }

		public FareModel Fare { get; set; } = new();

		[ForeignKey(nameof(Fare))]
		public int FareId { get; set; }

		public VehicleModel Vehicle { get; set; } = new();

		[ForeignKey(nameof(ParkingModel))]
		public int VehicleId { get; set; }

		public DateTime EntryTime { get; set; } = DateTime.Now;

		public DateTime? ExitTime
		{
			get => _exitTime;
			set
			{
				FareValidator.ValidateExitTime(EntryTime, value);

				_exitTime = value;
			}
		}

		public TimeSpan Duration => (ExitTime ?? DateTime.Now) - EntryTime;

		public decimal TotalPrice => FareValidator.CalculateTotalPrice(this);
	}
}
